using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using JobSeek.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aspose.Words;

namespace JobSeek.Areas.Recruiter.Controllers
{
    [Area("Recruiter")]
    public class CandidatesController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        private IWebHostEnvironment _webHostEnvironment;

        public CandidatesController(ApplicationDbContext db, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            Application application = new Application();
            var userId = _userManager.GetUserId(HttpContext.User);
            IEnumerable<Application> applications = application.GetApplicationsForRecruiter(userId,  _db);
            return View(applications);
        }

        //GET Details Action Method
        public IActionResult Details(string id)
        {
                var application = _db.Application.Include(c => c.Jobseeker).Include(c => c.JobOffer).Include(c => c.RecruiterUser)
                    .Include(c => c.JobOffer.JobType).Include(c => c.JobOffer.JobCategory)
                    .FirstOrDefault(c => c.Id == id);

                var resumePreview = ConvertToPdf(application.Resume);
                ViewBag.resumePreview = resumePreview;

                if (application.CoverLetter != null)
                {
                    var coverLetterPreview = ConvertToPdf(application.CoverLetter);
                    ViewBag.coverLetterPreview = coverLetterPreview;
                }
                application.Resume = "/" + application.Resume;
                application.CoverLetter = "/" + application.CoverLetter;

                if (id == null || application == null)
                {
                    return NotFound();
                }
                return View(application);
        }

        //METHOD
        public string ConvertToPdf(string filePath)
        {
            var serverPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
            var tempFolderPath = "Application/Temp Preview/";
            var dir = Path.Combine(_webHostEnvironment.WebRootPath, tempFolderPath);
            Document doc = new Document(serverPath);

            Random rnd = new Random();
            var tempFileName = rnd.Next().ToString() + ".pdf";
            doc.Save(dir + tempFileName);

            var tempFile = "/" + tempFolderPath + tempFileName;
            return tempFile;

        }
    }
}
