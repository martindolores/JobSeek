using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobSeek.Areas.Jobseeker.Controllers
{
    [Area("Jobseeker")]
    public class UserController : Controller
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobseekerUser jobseeker)
        {
            {
                if (ModelState.IsValid)
                {
                    jobseeker.Email = jobseeker.UserName;
                    jobseeker.NormalizedEmail = jobseeker.Email.ToUpper();
                    jobseeker.EmailConfirmed = true;
                    var result = await _userManager.CreateAsync(jobseeker, jobseeker.PasswordHash);
                    if (result.Succeeded)
                    {

                        var isSaveRole = await _userManager.AddToRoleAsync(jobseeker, role: "Jobseeker");
                        TempData["save"] = "User has been created successfully!";
                        return RedirectToAction("Index", "Home", new { area = "Jobseeker" });
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
                return View();
            }
        }
    }
}
