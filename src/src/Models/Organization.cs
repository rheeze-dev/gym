﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Organization : BaseEntity
    {
        public Organization()
        {
            this.thumbUrl = "/images/blank-building.png";
            
        }
        public Guid organizationId { get; set; }
        [Display(Name = "Organization Name")]
        [Required]
        [StringLength(100)]
        public string organizationName { get; set; }
        [StringLength(200)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [StringLength(255)]
        [Display(Name = "Thumb URL")]
        public string thumbUrl { get; set; }

        //refer to Application User
        public string organizationOwnerId { get; set; }

    }
}
