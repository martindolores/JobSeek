using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobSeek.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                TempData["exist"] = "This role already exists!";
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been created successfully!";
                return RedirectToAction(actionName: nameof(Index));
            }
            return View();
        }

        //GET Edit Action Method
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        //POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            var result = await _roleManager.UpdateAsync(role);
            if (role == null)
            {
                return NotFound();
            }
            else if (isExist)
            {
                TempData["exist"] = "This role already exists!";
                return View();
            }
            else if (result.Succeeded)
            {
                TempData["save"] = "Role has been updated successfully!";
                return RedirectToAction(actionName: nameof(Index));
            }
            return View();            
        }

        //GET Delete Action Method
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        //POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["delete"] = "Role has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
