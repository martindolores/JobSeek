using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.JobTypes = _db.JobTypes.ToList();
            ViewBag.JobCategory = _db.JobCategories.ToList();
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
        public IActionResult Details()
        {
            return View();
        }
    }
}

