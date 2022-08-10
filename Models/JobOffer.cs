using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobSeek.Models
{
    public class JobOffer
    {
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Job Type")]
        public int JobTypeId { get; set; }
        [ForeignKey("JobTypeId")]
        public JobType JobType { get; set; }
        [Display(Name = "Job Category")]
        public int JobCategoryId { get; set; }
        [ForeignKey("JobCategoryId")]
        [Display(Name = "Job Category")]
        public JobCategory JobCategory { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Submitted { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Wage { get; set; }
       
        


    }
}
