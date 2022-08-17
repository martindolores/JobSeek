using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobSeek.Areas.Recruiter.Controllers
{
    [Area("Recruiter")]
    public class CandidatesController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;

        public CandidatesController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var candidates = _db.Application.ToList();
            var jobOffers = _db.JobOffer.ToList();
            var jobSeekers = _db.JobseekerUser.ToList();
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewBag.userId = userId;
            ViewBag.jobOffers = jobOffers;
            ViewBag.JobSeekers = jobSeekers;
            return View(candidates);
        }
    }
}
