using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobSeek.Areas.Recruiter.Controllers
{
    [Area("Recruiter")]
    public class JobOfferController : Controller
    {
        ApplicationDbContext _db;

        public JobOfferController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           var jobOffer = _db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory).ToList();
           return View(jobOffer);
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            ViewData["jobTypeId"] = new SelectList(_db.JobTypes.ToList(), "Id", "JobTypes");
            ViewData["jobCategoryId"] = new SelectList(_db.JobCategories.ToList(), "Id", "JobCategories");
            ViewBag.DateNow = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobOffer jobOffer)
        {
            if (ModelState.IsValid)
            {
                var searchOffer = _db.JobOffer.FirstOrDefault(c => c.Title == jobOffer.Title);
                if (searchOffer != null)
                {
                    TempData["exist"] = "This Job Offer already exists.";
                    ViewBag.Message = "This Job Offer already exists.";
                    ViewData["jobTypeId"] = new SelectList(_db.JobTypes.ToList(), "Id", "JobTypes");
                    ViewData["jobCategoryId"] = new SelectList(_db.JobCategories.ToList(), "Id", "JobCategories");
                    return View(jobOffer);
                }
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
