using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace JobSeek.Controllers
{
    [Area("Jobseeker")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        private IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var jobOffers = _db.JobOffer.ToList();
            ViewBag.JobTypes = _db.JobTypes.ToList();
            ViewBag.JobCategory = _db.JobCategories.ToList();
            ViewBag.JobCount = jobOffers.Count();
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var role = await _userManager.GetRolesAsync(user);
                ViewBag.Role = role[0];
                return View();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //POST Search Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string jobTitle, string jobLoc)
        {
            var jobListings = _db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory).ToList();
            List<JobOffer> searchListing = new List<JobOffer>();
            if (!String.IsNullOrEmpty(jobTitle))
            {
                jobTitle.ToLower();
            }
            if (!String.IsNullOrEmpty(jobTitle) && !String.IsNullOrEmpty(jobLoc))
            {
                int? postCode = Int32.Parse(jobLoc);
                if (postCode.HasValue)
                {
                    foreach (var job in jobListings)
                    {
                        var jobSearch = job.Title.ToLower();
                        if (jobSearch.Contains(jobTitle) && job.PostalCode == jobLoc)
                        {
                            searchListing.Add(job);
                        }
                    }

                    return View(searchListing);
                }
            }
            else if (!String.IsNullOrEmpty(jobTitle) && String.IsNullOrEmpty(jobLoc))
            {
                foreach (var job in jobListings)
                {
                    var jobSearch = job.Title.ToLower();
                    if (jobSearch.Contains(jobTitle))
                    {
                        searchListing.Add(job);
                    }
                }
                return View(searchListing);
            }
            else if (String.IsNullOrEmpty(jobTitle) && !String.IsNullOrEmpty(jobLoc))
            {
                foreach (var job in jobListings)
                {
                    int? postCode = Int32.Parse(jobLoc);
                    if (job.PostalCode == jobLoc && postCode.HasValue)
                    {
                        searchListing.Add(job);
                    }
                }
                return View(searchListing);
            }
            return View(jobListings);
        }

        

        //GET Details Action Method
        public IActionResult Details(int? id)
        {
            var jobOffer = _db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory)
                .FirstOrDefault(c => c.Id == id);
            var submissionDate = jobOffer.Submitted.ToString("dd/MM/yyyy");
            ViewBag.JobCat = jobOffer.JobCategory.JobCategories;
            ViewBag.JobType = jobOffer.JobType.JobTypes;
            ViewBag.subDate = submissionDate;
            if (id == null || jobOffer == null)
            {
                return NotFound();
            }
            return View(jobOffer);
        }

        //GET Apply Action Method
        public IActionResult Apply(int? id)
        {
            var jobOffer = _db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory)
                .FirstOrDefault(c => c.Id == id);
            var userId = _userManager.GetUserId(HttpContext.User);
            List<JobseekerUser> jobseekers = _db.JobseekerUser.ToList();
            
            if (userId != null)
            {
                foreach (var jobseeker in jobseekers)
                {
                    if (jobseeker.Id == userId)
                    {
                        ViewBag.JobseekerId = jobseeker.Id;
                        ViewBag.FirstName = jobseeker.FirstName;
                        ViewBag.LastName = jobseeker.LastName;
                        ViewBag.Email = jobseeker.Email;
                        ViewBag.PhoneNumber = jobseeker.PhoneNumber;
                    }
                }
            }
            ViewBag.JobCat = jobOffer.JobCategory.JobCategories;
            ViewBag.JobType = jobOffer.JobType.JobTypes;
            ViewBag.TitleName = jobOffer.Title;
            ViewBag.Company = jobOffer.CompanyName;
            ViewBag.Wage = jobOffer.Wage;
            ViewBag.Description = jobOffer.Description;
            ViewBag.JobOfferId = jobOffer.Id;
            if (id == null || jobOffer == null)
            {
                return NotFound();
            }
            return View();
        }

        //POST Apply Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(Application application, IFormFile resume, IFormFile coverLetter)
        {
            string[] permittedExtensions = { ".txt", ".pdf", ".doc", ".docx" };
            
            if (application == null)
            {
                return NotFound();
            }

            
            if (resume == null)
            {  
                    TempData["required"] = "Please Upload a Resume!";
                    return RedirectToAction("Apply", new { id = application.JobOfferId });
            }

            if (resume != null)
            {
                var resumeExt = Path.GetExtension(resume.FileName).ToLowerInvariant();
                if (String.IsNullOrEmpty(resumeExt) || !permittedExtensions.Contains(resumeExt))
                {
                    TempData["resume"] = "Invalid Resume File Type!";
                    return RedirectToAction("Apply", new { id = application.JobOfferId });
                }
                string resumeFolder = "Application/CV/";
                application.Resume = await UploadFile(resumeFolder, resume);
            }
            

            if (coverLetter != null)
            {
                var coverLetterext = Path.GetExtension(coverLetter.FileName).ToLowerInvariant();

                if (String.IsNullOrEmpty(coverLetterext) || !permittedExtensions.Contains(coverLetterext))
                {
                    TempData["coverLetter"] = "Invalid Cover Letter File Type!";
                    return RedirectToAction("Apply", new { id = application.JobOfferId });
                }

                string coverLetterFolder = "Application/Cover Letter/";
                application.CoverLetter = await UploadFile(coverLetterFolder, coverLetter);
            }

            var errors = ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            application.Id = RandomUniqueNumber();

           

            

            using (var transaction = _db.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    _db.Application.Add(application);
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Application ON;");
                    await _db.SaveChangesAsync();
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Application OFF;");
                    transaction.Commit();
                    TempData["submitted"] = "Application has successfully submitted!";
                    return RedirectToAction(actionName: nameof(Index));
                }
            }
            return View();
        }

        //METHODS
        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, Path.GetFileName(file.FileName));
            using (FileStream fs = new FileStream(serverFolder, FileMode.Create))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
            }
                
            GC.Collect();
            return folderPath + file.FileName;
        }

        private int RandomUniqueNumber()
        {
            Random rnd = new Random();
            int uniqueId = rnd.Next();
            var existingApplication = _db.Application.FirstOrDefault(c => c.Id == uniqueId);
            if (existingApplication == null)
            {
                return uniqueId;
            }
            else
            {
                return uniqueId += RandomUniqueNumber();
            }

            
        }
    }
}

