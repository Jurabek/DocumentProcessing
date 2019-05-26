using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentProcessing.Data;
using DocumentProcessing.Models;
using DocumentProcessing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DocumentProcessing.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
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
        
        public IActionResult Index()
        {
            var documents = _context.Documents
                .Include(x => x.Owner)
                .Include(x => x.Applicant)
                .Include(x => x.Recipient)
                .Include(x => x.Purpose)
                .Include(x => x.Status);
            
            var list = _mapper.Map<IEnumerable<DocumentListViewModel>>(documents);
            
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateOwnersDropDownList(_context.DocumentOwners.FirstOrDefault());
            PopulateApplicantsDropDownList();
            
            PopulateStatusesDropDownList();
            PopulatePurposesDropDownList();
            
            var vm = BuildDocumentViewModel();
            
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentViewModel viewModel)
        {
            if (viewModel.ApplicantId == null || string.IsNullOrEmpty(viewModel.Applicant))
            {
                ModelState.AddModelError("", "Номи ташкилот холи аст!");
            }
            
            if (ModelState.IsValid)
            {
                
                var document = _mapper.Map<Document>(viewModel);
                document.Date = DateTime.Now;
                
                await _context.AddAsync(document);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }
               
            }

            var selectedOwner = _context.DocumentOwners.FirstOrDefault(x => x.Id == viewModel.OwnerId);
            
            PopulateOwnersDropDownList(selectedOwner);
            PopulateApplicantsDropDownList();
            
            PopulateStatusesDropDownList();
            PopulatePurposesDropDownList();

            return View(viewModel);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _context.Documents
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            var viewModel = _mapper.Map<DocumentViewModel>(document);
            viewModel.ApplicantType = ApplicantType.Existing;
            
            SetSelectedDropDownLists(document);
            
            return View(viewModel);
        }

        private void SetSelectedDropDownLists(Document document)
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

        private DocumentViewModel BuildDocumentViewModel()
        {
            var vm = new DocumentViewModel();
            return vm;
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

        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}