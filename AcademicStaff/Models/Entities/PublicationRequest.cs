using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicStaff.Models.Entities
{
    public class PublicationRequest
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Display(Name = "Your Email")]
        public string Email { get; set; }

        [Display(Name = "Your Message")]
        public string Message { get; set; }

        [Display(Name = "Paper Title")]
        public int? PublicationId { get; set; }
        public Publication Publication { get; set; }

        [Display(Name = "Staff Name")]
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }


    }
}