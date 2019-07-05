using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademicStaff.Models.Entities
{
    public class Publication
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Paper Title")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [AllowHtml]
        public string Abstract { get; set; }

       
        public string Image { get; set; }

        [Display(Name = "Staff Name")]
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }

        public DateTime DateCreated { get; set; }


        public static List<Publication> GetPublication()
        {
            var db = new ApplicationDbContext();
            return db.Publications.ToList();

        }

       
    }
}