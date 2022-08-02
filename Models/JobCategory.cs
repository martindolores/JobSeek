using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobSeek.Models
{
    public class JobCategory
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Job Category")]
        public string JobCategories { get; set; }
    }
}
