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
    [Authorize(Roles = "Admin")]

    public class JobTypeController : Controller
    {
        private ApplicationDbContext _db;
        
        public JobTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.JobTypes.ToList());
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobType jobType)
        {
            if (ModelState.IsValid)
            {
                _db.JobTypes.Add(jobType);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobType);
        }

        //GET Edit Action Method
        public IActionResult Edit(int? id)
        {
            var jobType = _db.JobTypes.Find(id);
            if (id == null || jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        //POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobType jobType)
        {
            if (ModelState.IsValid)
            {
                _db.JobTypes.Update(jobType);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobType);
        }

        //GET Details Action Method
        public IActionResult Details(int? id)
        {
            var jobType = _db.JobTypes.Find(id);
            if (id == null || jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        //POST Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(JobType jobType)
        {
            return RedirectToAction(actionName: nameof(Index));
        }

        //GET Delete Action Method
        public IActionResult Delete(int? id)
        {
            var jobType = _db.JobTypes.Find(id);
            if (id == null || jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        //POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(JobType jobType)
        {
            if (ModelState.IsValid)
            {
                _db.JobTypes.Remove(jobType);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(jobType);
        }
    }
}
