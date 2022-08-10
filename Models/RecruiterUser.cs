﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JobSeek.Models
{
    public class RecruiterUser : IdentityUser
    {
        [Required]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
       
    }
}
