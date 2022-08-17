using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JobSeek.Models
{
    public class Application
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Job Offer")]
        public int JobOfferId { get; set; }
        [Required]
        [Display(Name = "Candidate")]
        public string JobseekerId { get; set; }
        public string Resume { get; set; }
        public string CoverLetter { get; set; }

    }
}
