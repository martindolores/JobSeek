using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using JobSeek.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace JobSeek.Models
{
    public class Application
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Job Offer")]
        public int? JobOfferId { get; set; }
        [Required]
        [Display(Name = "Candidate")]
        public string JobseekerId { get; set; }
        [Display(Name = "Recruiter")]
        public string RecruiterId { get; set; }
        public string Resume { get; set; }
        
        public string CoverLetter { get; set; }
        [ForeignKey("JobOfferId")]
        public JobOffer JobOffer {get;set;}
        [ForeignKey("JobseekerId")]
        public JobseekerUser Jobseeker { get; set; }
        [ForeignKey("RecruiterId")]
        public RecruiterUser RecruiterUser { get; set; }

        public JobOffer GetJobOffer(int? jobOfferId, ApplicationDbContext db)
        {
            if (jobOfferId == null)
            {
                return null;
            }
            var jobOffer = db.JobOffer.Include(c => c.JobType).Include(c => c.JobCategory)
                .FirstOrDefault(c => c.Id == jobOfferId);
            return jobOffer;
        }

        public JobseekerUser GetJobseeker(string jobseekerId, ApplicationDbContext db)
        {
            var jobseekerUser = db.JobseekerUser.FirstOrDefault(c => c.Id == jobseekerId);
            return jobseekerUser;
        }
        public RecruiterUser GetRecruiter(string recruiterId, ApplicationDbContext db)
        {
            var recruiterUser = db.RecruiterUser.FirstOrDefault(c => c.Id == recruiterId);
            return recruiterUser;
        }

        public IEnumerable<Application> GetApplicationsForRecruiter(string recruiterId, ApplicationDbContext db)
        {
            var applications = db.Application.Include(c => c.Jobseeker).Include(c => c.JobOffer).Include(c => c.RecruiterUser).ToList();
            IEnumerable<Application> recruiterApplications;
            List<Application> applicationsForRecruiter = new List<Application>();
            foreach (var application in applications)
            {
                if (application.JobOffer.RecruiterId == recruiterId)
                {
                    applicationsForRecruiter.Add(application);
                }
            }
            recruiterApplications = applicationsForRecruiter.AsEnumerable();
            return recruiterApplications;
        }
    }
}
