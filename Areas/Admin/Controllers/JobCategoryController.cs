using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSeek.Areas.Recruiter.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class JobCategoryController : Controller
    {
        private ApplicationDbContext _db;

        public JobCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.JobCategories.ToList());  
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                _db.JobCategories.Add(jobCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobCategory);
        }

        //GET Edit Action Method
        public IActionResult Edit(int? id)
        {
            var jobCategory = _db.JobCategories.Find(id);
            if (id == null || jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        //POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                _db.JobCategories.Update(jobCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobCategory);
        }

        //GET Details Action Method
        public IActionResult Details(int? id)
        {
            var jobCategory = _db.JobCategories.Find(id);
            if (id == null || jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        //POST Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(JobCategory jobCategory)
        {
            return RedirectToAction(actionName: nameof(Index));
        }

        //GET Delete Action Method
        public IActionResult Delete(int? id)
        {
            var jobCategory = _db.JobCategories.Find(id);
            if (id == null || jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        //POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                _db.JobCategories.Remove(jobCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobCategory);
            
        }
    }
}
