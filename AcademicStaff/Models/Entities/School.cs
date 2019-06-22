using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string Image { get; set; } = "~/Content/AdminLTE/dist/img/bg-6.jpg";

        public virtual ICollection<Department> Department { get; set; }


        public static List<School> GetSchools()
        {
            var db = new ApplicationDbContext();
            return db.Schools.ToList();

        }
    }
}