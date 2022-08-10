using System;
using System.Collections.Generic;
using System.Text;
using JobSeek.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobSeek.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobOffer> JobOffer { get; set; }
        public DbSet<RecruiterUser> RecruiterUser { get; set; }
    }
}
