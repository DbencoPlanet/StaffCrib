using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademicStaff.Models.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Photo { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "State of Origin")]
        public string StateOfOrigin { get; set; }

        //[Display(Name = "Email Address")]
        //public string EmailAddres { get; set; }

        [Display(Name = "Local Government of Origin")]
        public string LocalGov { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname field is required")]
        public string Surname { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name field is required")]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherNames { get; set; }

        public string Fullname
        {
            get
            {
                return Surname + " " + FirstName + " " + OtherNames;
            }
        }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Current Appt date")]
        public DateTime? CurrentAppDate { get; set; }

        [UIHint("Enum")]
        [Display(Name = "Rank")]
        public StaffRank Rank { get; set; }

        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }

        public string Religion { get; set; }

        [Display(Name = "Resident Address")]
        public string Address { get; set; }

        [Display(Name = "School")]
        public int? SchoolId { get; set; }

        [Display(Name = "School")]
        public School School { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Department")]
        public Department Department { get; set; }

        [Display(Name = "Staff No")]
        public string StaffNo { get; set; }


        [Display(Name = "Biography")]
        [AllowHtml]
        public string Biography { get; set; }


        [UIHint("Enum")]
        [Display(Name = "Marital Status")]
        public MaritalStatus MaritalStatus { get; set; }


        [UIHint("Enum")]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        public virtual ICollection<Publication> Publication { get; set; }

        public virtual ICollection<PublicationRequest> PublicationRequest { get; set; }

        public static List<Profile> GetProfiles()
        {
            var db = new ApplicationDbContext();
            return db.Profiles.ToList();

        }

    }
}