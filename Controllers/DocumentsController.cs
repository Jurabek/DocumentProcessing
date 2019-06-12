using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentProcessing.Abstraction;
using DocumentProcessing.Data;
using DocumentProcessing.Models;
using DocumentProcessing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DocumentProcessing.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private const int PageSize = 3;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public DocumentsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "q")] string searchText, int? pageNumber)
        {
            var documents = _context.Documents
                .Include(x => x.Applicant)
                .Include(x => x.Purpose)
                .Include(x => x.Status)
                .Include(x => x.ScannedFiles)
                .Include(x => x.Owner)
                .Include(x => x.Recipient);

            if (!string.IsNullOrEmpty(searchText))
            {
                ViewBag.SearchText = searchText;

                var filteredDocuments = documents
                    .Where(x => x.Applicant.Name.CaseInsensitiveContains(searchText)
                                || x.Owner.Name.CaseInsensitiveContains(searchText)
                                || x.EntryNumber.ToString(CultureInfo.InvariantCulture) == searchText
                                || x.AppointmentNumber == searchText
                                || x.Recipient.Name.CaseInsensitiveContains(searchText)
                                || x.Purpose.Name.CaseInsensitiveContains(searchText)
                                || x.Status.Name.CaseInsensitiveContains(searchText));

                var result = await MappedPaginatedList<DocumentListViewModel>
                    .CreateAsync(filteredDocuments.AsNoTracking(), _mapper, pageNumber ?? 1, PageSize);

                return View(result);
            }

            var list = await MappedPaginatedList<DocumentListViewModel>
                    .CreateAsync(documents.AsNoTracking(), _mapper, pageNumber ?? 1, PageSize);
                
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var selectedOwner = await _context.DocumentOwners.FirstOrDefaultAsync();
            
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
                    document.ScannedFiles = await GetScannedFiles(files);
                }

                await _context.AddAsync(document);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }
            }

            SetSelectedDropDownLists(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Progress()
        {
            return Content(Startup.Progress.ToString());
        }

        private static async Task<IList<ScannedFile>> GetScannedFiles(IList<IFormFile> files)
        {
            long totalBytes = files.Sum(f => f.Length);
            IList<ScannedFile> scannedFiles = new List<ScannedFile>();
            foreach (var f in files)
            {
                byte[] buffer = new byte[16 * 1024];
                using (var output = new MemoryStream())
                {
                    using (var input = f.OpenReadStream())
                    {
                        long totalReadBytes = 0;
                        int readBytes;

                        while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            await output.WriteAsync(buffer, 0, readBytes);
                            totalReadBytes += readBytes;
                            var progress = (int) ((float) totalReadBytes / (float) totalBytes * 100.0);
                            Startup.Progress = progress < 0 ? 0 : progress;
                            await Task.Delay(2);
                        }
                    }

                    scannedFiles.Add(new ScannedFile
                    {
                        FileName = f.FileName,
                        ContentType = f.ContentType,
                        Length = f.Length,
                        File = output.ToArray()
                    });
                }
            }

            return scannedFiles;
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _context.Documents
                .Include(x => x.ScannedFiles)
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
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == viewModel.Id);

                if (originalDocument == null)
                {
                    return NotFound();
                }

                await CreateApplicantIfNotExist(viewModel);
                var document = _mapper.Map<Document>(viewModel);

                var hasAddedFiles = files.Any();
                var hasDocumentChanges = HasChangesBetweenTwoDocuments(originalDocument, document);
                var hasRemovedFiles = viewModel.ScannedFiles.Any(x => x.IsDeleted);
                bool hasError = false;

                if (hasAddedFiles)
                {
                    var scannedFiles = await GetScannedFiles(files);
                    foreach (var scannedFile in scannedFiles)
                    {
                        scannedFile.DocumentId = originalDocument.Id;
                        await _context.ScannedFiles.AddAsync(scannedFile);
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                                                     "Try again, and if the problem persists, " +
                                                     "see your system administrator.");
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
                            .Select(id => new ScannedFile {Id = id});

                    try
                    {
                        _context.RemoveRange(deletedFiles);
                        _context.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                                                     "Try again, and if the problem persists, " +
                                                     "see your system administrator.");
                        hasError = true;
                    }
                }

                if (hasDocumentChanges)
                {
                    try
                    {
                        document.Date = originalDocument.Date;
                        _context.Update(document);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                                                     "Try again, and if the problem persists, " +
                                                     "see your system administrator.");
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

        private bool HasChangesBetweenTwoDocuments(Document originalDocument, Document document)
        {
            return originalDocument.ApplicantId != document.ApplicantId
                   || originalDocument.AppointmentNumber != document.AppointmentNumber
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
                ModelState.AddModelError("", "Номи ташкилот холи аст!");
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
                    var applicant = new Applicant {Name = viewModel.ApplicantName, Id = Guid.NewGuid()};
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
    }
}