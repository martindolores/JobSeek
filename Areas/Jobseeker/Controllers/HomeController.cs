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

        public IActionResult Index()
        {
            var jobOffers = _db.JobOffer.ToList();
            ViewBag.JobTypes = _db.JobTypes.ToList();
            ViewBag.JobCategory = _db.JobCategories.ToList();
            ViewBag.JobCount = jobOffers.Count();
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
            Application application = new Application();

            var jobSeekerId = _userManager.GetUserId(HttpContext.User);
            application.JobseekerId = jobSeekerId;
            application.JobOfferId = id;
            

            application.Jobseeker = application.GetJobseeker(application.JobseekerId, _db);
            application.JobOffer = application.GetJobOffer(application.JobOfferId, _db);


            application.RecruiterId = application.JobOffer.RecruiterId;
            application.RecruiterUser = application.GetRecruiter(application.RecruiterId, _db);
            if (id == null || application==null)
            {
                return NotFound();
            }
            return View(application);
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
                var resumePath = await UploadFile(resumeFolder, resume);
                long resumeSize = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, resumePath)).Length;
                if (resumeSize < 2097152) //2Mb
                {
                    
                    application.Resume = resumePath;
                }
                else
                {
                    TempData["resumeSize"] = $"{resume.FileName} is too big! Please choose smaller resume.";
                    return RedirectToAction("Apply", new { id = application.JobOfferId });
                }
                
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
                var coverLetterPath = await UploadFile(coverLetterFolder, coverLetter);
                long coverLetterSize = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, coverLetterPath)).Length;
                if (coverLetterSize < 2097152) //2Mb
                {
                    application.CoverLetter = await UploadFile(coverLetterFolder, coverLetter);
                }
                else
                {
                    TempData["coverLetterSize"] = $"{coverLetter.FileName} is too big! Please choose smaller cover letter";
                    return RedirectToAction("Apply", new { id = application.JobOfferId });
                }
                    
            }

            var errors = ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            application.Id = RandomUniqueString();

           

            

            using (var transaction = _db.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    _db.Application.Add(application);
                    await _db.SaveChangesAsync();
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
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            using (FileStream fs = new FileStream(serverFolder, FileMode.Create))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
            }
                
            GC.Collect();
            return folderPath;
        }

        private string RandomUniqueString()
        {
            var uniqueId = Guid.NewGuid().ToString();
            var existingApplication = _db.Application.FirstOrDefault(c => c.Id == uniqueId);
            if (existingApplication == null)
            {
                return uniqueId;
            }
            else
            {
                return uniqueId += RandomUniqueString();
            }

            
        }
    }
}

