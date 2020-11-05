using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace src.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(250)]
        [Display(Name = "Profile Picture")]
        public string ProfilePictureUrl { get; set; } = "/images/empty-profile.png";

        [StringLength(250)]
        [Display(Name = "Wallpaper Picture")]
        public string WallpaperPictureUrl { get; set; } = "/images/wallpaper1.jpg";

        public string IdNumber { get; set; }

        public bool IsSuperAdmin { get; set; } = false;

        public bool IsStudent { get; set; } = false;

        public bool IsFaculty { get; set; } = false;
        public DateTime DateTimeCreated { get; set; }
        public bool Active { get; set; }
    }

    //next task create subject content(quizes,activities), student grades
}
