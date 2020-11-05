using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<src.Models.Organization> Organization { get; set; }

        public DbSet<src.Models.Contact> Contact { get; set; }

        public DbSet<src.Models.Members> Members { get; set; }

        public DbSet<src.Models.Promos> Promos { get; set; }

        public DbSet<src.Models.DailyCollection> DailyCollection { get; set; }

        public DbSet<src.Models.MonthlyCollection> MonthlyCollection { get; set; }

        public DbSet<src.Models.Equipments> Equipments { get; set; }

        public DbSet<src.Models.Logins> Logins { get; set; }

        public DbSet<src.Models.Others> Others { get; set; }

        public DbSet<src.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
