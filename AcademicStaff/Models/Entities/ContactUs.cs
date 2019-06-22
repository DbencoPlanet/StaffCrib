using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicStaff.Models.Entities
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser user { get; set; }

        [Display(Name = "Staff Name")]
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
      
       
    }
}