using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using AcademicStaff.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AcademicStaff.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Surname")]
        public string Surname { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherNames { get; set; }

        [Display(Name = "User Status")]
        [UIHint("Enum")]
        public Status Status { get; set; }

    }

   

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

      
        public DbSet<States> States { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<LocalGovs> LocalGovs { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationRequest> PublicationRequests { get; set; }
        public DbSet<ImageGallery> ImageGallery { get; set; }
        public DbSet<ImageModel> ImageModel { get; set; }
        public DbSet<ImageSlider> ImageSlider { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}