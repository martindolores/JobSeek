using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
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
        public async Task<IActionResult> Apply(int? id)
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
                        ViewBag.FirstName = jobseeker.FirstName;
                        ViewBag.LastName = jobseeker.LastName;
                        ViewBag.Email = jobseeker.Email;
                        ViewBag.PhoneNumber = jobseeker.PhoneNumber;
                    }
                }
            }
            ViewBag.JobCat = jobOffer.JobCategory.JobCategories;
            ViewBag.JobType = jobOffer.JobType.JobTypes;
            if (id == null || jobOffer == null)
            {
                return NotFound();
            }
            return View(jobOffer);
        }

    }
}

