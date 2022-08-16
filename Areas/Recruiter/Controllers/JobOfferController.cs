using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace JobSeek.Areas.Recruiter.Controllers
{
    [Area("Recruiter")]
    public class JobOfferController : Controller
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;

        public JobOfferController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var jobOffer = _db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory).ToList();
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = userId;
            return View(jobOffer);
        }

        //GET Create Action Method
        public async Task<IActionResult> Create()
        {
            ViewData["jobTypeId"] = new SelectList(_db.JobTypes.ToList(), "Id", "JobTypes");
            ViewData["jobCategoryId"] = new SelectList(_db.JobCategories.ToList(), "Id", "JobCategories");
            //var userId = _userManager.GetUserId(HttpContext.User);
            //RecruiterUser user = _userManager.FindByIdAsync(userId).Result;
            var userId = _userManager.GetUserId(HttpContext.User);

            List<RecruiterUser> recruiters = _db.RecruiterUser.ToList();
            foreach (var recruiter in recruiters)
            {
                if (recruiter.Id == userId)
                {
                    ViewBag.RecruiterId = recruiter.Id;
                    ViewBag.Company = recruiter.CompanyName;
                }
            }
            ViewBag.DateNow = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobOffer jobOffer)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            List<RecruiterUser> recruiters = _db.RecruiterUser.ToList();
            foreach (var recruiter in recruiters)
            {
                if (recruiter.Id == userId)
                {
                    jobOffer.CompanyName = recruiter.CompanyName;
                }
            }
            if (ModelState.IsValid)
            {
                var date = DateTime.Now.ToString("dd/MM/yyyy");
                jobOffer.Submitted = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _db.JobOffer.Add(jobOffer);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobOffer);
        }

        //GET Edit Action Method
        public IActionResult Edit(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            List<RecruiterUser> recruiters = _db.RecruiterUser.ToList();
            foreach (var recruiter in recruiters)
            {
                if (recruiter.Id == userId)
                {
                    ViewBag.RecruiterId = recruiter.Id;
                    ViewBag.Company = recruiter.CompanyName;
                }
            }
            var jobOffer = _db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory)
                .FirstOrDefault(c => c.Id == id);
            ViewData["jobTypeId"] = new SelectList(_db.JobTypes.ToList(), "Id", "JobTypes");
            ViewData["jobCategoryId"] = new SelectList(_db.JobCategories.ToList(), "Id", "JobCategories");
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.DateNow = date;
            if (id == null || jobOffer == null)
            {
                return NotFound();
            }
            return View(jobOffer);
        }

        //POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobOffer jobOffer)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            List<RecruiterUser> recruiters = _db.RecruiterUser.ToList();
            foreach (var recruiter in recruiters)
            {
                if (recruiter.Id == userId)
                {
                    jobOffer.CompanyName = recruiter.CompanyName;
                }
            }
            if (ModelState.IsValid)
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                jobOffer.Submitted = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _db.JobOffer.Update(jobOffer);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobOffer);
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

        //GET Delete Action Method
        public IActionResult Delete(int? id)
        {
            var jobOffer = _db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory)
                .FirstOrDefault(c => c.Id == id);
            var submissionDate = jobOffer.Submitted.ToString("dd/MM/yyyy");
            ViewBag.subDate = submissionDate;
            ViewBag.JobCat = jobOffer.JobCategory.JobCategories;
            ViewBag.JobType = jobOffer.JobType.JobTypes;
            if (id == null || jobOffer == null)
            {
                return NotFound();
            }
            return View(jobOffer);
        }

        //POS Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(JobOffer jobOffer)
        {
            if (ModelState.IsValid)
            {
                _db.JobOffer.Remove(jobOffer);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobOffer);
        }
    }
}
