using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademicStaff.Models.Entities
{
    public class School
    {
        public int? Id { get; set; }

        [Display(Name = "School Name")]
        public string Name { get; set; }

        [Display(Name = "School Code")]
        public string ShortCode { get; set; }

      

        [Display(Name = "Image")]
        public string Image { get; set; }

     

        public virtual ICollection<Department> Department { get; set; }


        public static List<School> GetSchools()
        {
            var db = new ApplicationDbContext();
            return db.Schools.ToList();

        }
    }
}