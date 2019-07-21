using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DocumentProcessing.Abstraction;
using DocumentProcessing.Data;
using DocumentProcessing.Helpers;
using DocumentProcessing.Models;
using DocumentProcessing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;

namespace DocumentProcessing.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private const int PageSize = 10;

        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;
        private readonly IFileUploader _fileUploader;
        private readonly IElectronicStamp _electronicStamp;
        private readonly ILogger<DocumentsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public DocumentsController(
            IFileUploader fileUploader,
            IElectronicStamp electronicStamp,
            ILogger<DocumentsController> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _fileUploader = fileUploader;
            _electronicStamp = electronicStamp;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery(Name = "q")] string searchText,
            [FromQuery(Name = "dateFrom")] string startDate,
            [FromQuery(Name = "dateFor")] string endDate,
            int? pageNumber)
        {
            var documents = _context.Documents.OrderByDescending(x => x.Date)
                .Include(x => x.Applicant)
                .Include(x => x.Purpose)
                .Include(x => x.Status)
                .Include(x => x.ScannedFiles)
                .Include(x => x.Owner)
                .Include(x => x.Recipient)
                .Include(x => x.Appointment).AsQueryable();

            if (!string.IsNullOrEmpty(endDate) && !string.IsNullOrEmpty(startDate))
            {
                var parsedStartDate = DateTime.ParseExact(startDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                var parsedEndDate = DateTime.ParseExact(endDate, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                
                ViewBag.DateFor = endDate;
                ViewBag.DateFrom = startDate;
                documents = documents.Where(x => x.Date >= parsedStartDate && x.Date < parsedEndDate);
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;
                documents = documents.Where(Search(searchText));
            }

            _logger.LogInformation(documents.ToSql());

            var list = await MappedPaginatedList<DocumentListViewModel>
                .CreateAsync(documents, _mapper, pageNumber ?? 1, PageSize);

            return View(list);
        }

        private Expression<Func<Document, bool>> Search(string searchText)
        {
            var upperSearchText = searchText.ToUpperInvariant();
            
            return x => x.Applicant.Name.ToUpperInvariant().Contains(upperSearchText)
                        || x.Owner.Name.ToUpperInvariant().Contains(upperSearchText)
                        || x.EntryNumber.ToString(CultureInfo.InvariantCulture).Equals(searchText)
                        || x.Applicant.Name.ToUpperInvariant().Contains(upperSearchText)
                        || x.Recipient.Name.ToUpperInvariant().Contains(upperSearchText)
                        || x.Purpose.Name.ToUpperInvariant().Contains(upperSearchText)
                        || x.Status.Name.ToUpperInvariant().Contains(upperSearchText);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var selectedOwner = await _context.DocumentOwners
                .OrderBy(x => x.Name)
                .FirstOrDefaultAsync();

            PopulateOwnersDropDownList(selectedOwner);
            PopulateApplicantsDropDownList();
            PopulateStatusesDropDownList();
            PopulatePurposesDropDownList();

            return View(new DocumentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel viewModel, IList<IFormFile> files)
        {
            ValidateApplicant(viewModel);

            if (ModelState.IsValid)
            {
                await CreateApplicantIfNotExist(viewModel);
                var document = _mapper.Map<Document>(viewModel);

                if (files.Any())
                {
                    var newFiles = await _fileUploader.GetScannedFilesForDocument(document, files);
                    
                    document.ScannedFiles = newFiles;
                }

                await _context.AddAsync(document);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (!CheckConstraintException(ex))
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                                                     "Try again, and if the problem persists, " +
                                                     "see your system administrator.");
                    }
                }
            }

            SetSelectedDropDownLists(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Progress()
        {
            var progressResult = HttpContext.Session.GetInt32("progress").ToString();
            return Content(progressResult);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _context.Documents
                .Include(x => x.ScannedFiles)
                .Include(x => x.Appointment)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<DocumentViewModel>(document);
            viewModel.ApplicantType = ApplicantType.Existing;

            SetSelectedDropDownLists(document);

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(DocumentViewModel viewModel, IList<IFormFile> files)
        {
            ValidateApplicant(viewModel);

            if (ModelState.IsValid)
            {
                var originalDocument = _context.Documents
                    .Include(x => x.Appointment)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == viewModel.Id);
                
                if (originalDocument == null)
                {
                    return NotFound();
                }

                await CreateApplicantIfNotExist(viewModel);
                var document = _mapper.Map<Document>(viewModel);
                document.Date = originalDocument.Date;
                
                var hasAddedFiles = files.Any();
                var hasDocumentChanges = HasChangesBetweenTwoDocuments(originalDocument, document);
                var hasRemovedFiles = viewModel.ScannedFiles.Any(x => x.IsDeleted);
                bool hasError = false;

                if (hasAddedFiles)
                {
                    try
                    {
                        var scannedFiles = await _fileUploader.GetScannedFilesForDocument(document, files);
                        foreach (var scannedFile in scannedFiles)
                        {
                            scannedFile.DocumentId = originalDocument.Id;
                            await _context.ScannedFiles.AddAsync(scannedFile);
                        }
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        if (!CheckConstraintException(ex))
                        {
                            ModelState.AddModelError("", "Unable to save changes. " +
                                                         "Try again, and if the problem persists, " +
                                                         "see your system administrator.");
                        }
                        hasError = true;
                    }
                }

                if (hasRemovedFiles)
                {
                    var deletedScannedViewModels = viewModel
                        .ScannedFiles
                        .Where(x => x.IsDeleted)
                        .Select(x => x.Id);

                    var deletedFiles =
                        _context.ScannedFiles.Select(x => x.Id)
                            .Where(id => deletedScannedViewModels.Contains(id))
                            .Select(id => new ScannedFile { Id = id });

                    try
                    {
                        _context.RemoveRange(deletedFiles);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        if (!CheckConstraintException(ex))
                        {
                            ModelState.AddModelError("", "Unable to save changes. " +
                                                     "Try again, and if the problem persists, " +
                                                     "see your system administrator.");
                        }
                        hasError = true;
                    }
                }

                if (hasDocumentChanges)
                {
                    try
                    {
                        if (originalDocument.Appointment == null)
                        {
                            var createdAppointment = document.Appointment;
                            createdAppointment.DocumentId = originalDocument.Id;
                            
                            _context.Add(document.Appointment);
                            _context.SaveChanges();
                        }
                        
                        _context.Update(document);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        if (!CheckConstraintException(ex))
                        {
                            ModelState.AddModelError("", "Unable to save changes. " +
                                                         "Try again, and if the problem persists, " +
                                                         "see your system administrator.");
                        }
                        hasError = true;
                    }
                }

                if ((hasAddedFiles || hasDocumentChanges || hasRemovedFiles) && !hasError)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Has not changes!");
            }

            SetSelectedDropDownLists(viewModel);
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _context.Documents
                .Where(x => x.Id == id)
                .Include(x => x.Owner)
                .Include(x => x.Applicant)
                .Include(x => x.Recipient)
                .Include(x => x.Purpose)
                .Include(x => x.Status)
                .FirstOrDefault();

            var result = _mapper.Map<DocumentListViewModel>(document);

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var course = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult File(Guid id)
        {
            var file = _context.ScannedFiles.FirstOrDefault(x => x.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return File(file.File, file.ContentType, file.FileName);
        }

        private bool HasChangesBetweenTwoDocuments(Document originalDocument, Document document)
        {
            return originalDocument.ApplicantId != document.ApplicantId
                   || originalDocument.Appointment?.Character != document.Appointment.Character
                   || originalDocument.Appointment?.Number != document.Appointment.Number
                   || originalDocument.EntryNumber != document.EntryNumber
                   || originalDocument.OwnerId != document.OwnerId
                   || originalDocument.PurposeId != document.PurposeId
                   || originalDocument.StatusId != document.StatusId
                   || originalDocument.ApplicantId != document.ApplicantId;
        }

        private void ValidateApplicant(DocumentViewModel viewModel)
        {
            if (viewModel.ApplicantId == null && string.IsNullOrEmpty(viewModel.ApplicantName))
            {
                ModelState.AddModelError("ApplicantName", "Номи ташкилот холи аст!");
            }
        }

        private void SetSelectedDropDownLists(IDocumentModel document)
        {
            var selectedPurpose = _context.Purposes
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == document.PurposeId);

            var selectedStatus = _context.Statuses
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == document.StatusId);

            var selectedOwner = _context.DocumentOwners
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == document.OwnerId);

            var selectedApplicant = _context.Applicants
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == document.ApplicantId);

            PopulateApplicantsDropDownList(selectedApplicant);
            PopulateOwnersDropDownList(selectedOwner);
            PopulatePurposesDropDownList(selectedPurpose);
            PopulateStatusesDropDownList(selectedStatus);
        }

        private async Task CreateApplicantIfNotExist(DocumentViewModel viewModel)
        {
            if (viewModel.ApplicantType == ApplicantType.New)
            {
                var applicantExist = await _context.Applicants
                    .FirstOrDefaultAsync(x => x.Name == viewModel.ApplicantName);

                if (applicantExist == null)
                {
                    var applicant = new Applicant { Name = viewModel.ApplicantName, Id = Guid.NewGuid() };
                    await _context.Applicants.AddAsync(applicant);
                    await _context.SaveChangesAsync();
                    viewModel.ApplicantId = applicant.Id;
                }
                else
                {
                    viewModel.ApplicantId = applicantExist.Id;
                }
            }
        }

        private void PopulatePurposesDropDownList(object selectedPurpose = null)
        {
            ViewBag.Purposes = new SelectList(_context.Purposes.AsNoTracking(),
                "Id",
                "Name",
                selectedPurpose);
        }

        private void PopulateStatusesDropDownList(object selectedStatus = null)
        {
            ViewBag.Statuses = new SelectList(_context.Statuses.AsNoTracking(),
                "Id",
                "Name",
                selectedStatus);
        }

        private void PopulateOwnersDropDownList(object selectedOwner = null)
        {
            ViewBag.Owners = new SelectList(_context.DocumentOwners.AsNoTracking(),
                "Id",
                "Name",
                selectedOwner);
        }

        private void PopulateApplicantsDropDownList(object selectedApplicant = null)
        {
            ViewBag.Applicants = new SelectList(_context.Applicants.AsNoTracking(),
                "Id",
                "Name",
                selectedApplicant);
        }

        private bool CheckConstraintException(DbUpdateException ex)
        {
            var sqlEx = ex?.InnerException as SqlException;
            if (sqlEx != null)
            {
                //This is a DbUpdateException on a SQL database

                if (sqlEx.Number == SqlServerViolationOfUniqueIndex ||
                    sqlEx.Number == SqlServerViolationOfUniqueConstraint)
                {
                    ModelState.AddModelError("", "Рақами воридотӣ мавҷуд аст");
                }
                return true;
            }
            return false;
        }
    }
}