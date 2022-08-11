using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobSeek.Areas.Recruiter.Controllers
{
    [Area("Recruiter")]
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


        //
        // NEED TO FINISH ROLE CONTROLLER AND VIEWS FIRST   
        //

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecruiterUser recruiter)
        {
            {
                if (ModelState.IsValid)
                {
                    recruiter.Email = recruiter.UserName;
                    recruiter.NormalizedEmail = recruiter.Email.ToUpper();
                    recruiter.EmailConfirmed = true;
                    var result = await _userManager.CreateAsync(recruiter, recruiter.PasswordHash);
                    if (result.Succeeded)
                    {

                        var isSaveRole = await _userManager.AddToRoleAsync(recruiter, role: "Recruiter");
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
