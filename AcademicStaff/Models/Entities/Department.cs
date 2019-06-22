using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicStaff.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Dept Name")]
        public string Name { get; set; }

        [Display(Name = "Dept Code")]
        public string ShortCode { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; } = "~/Content/AdminLTE/dist/img/bg-6.jpg";

        public int? SchoolId { get; set; }
        public School School { get; set; }


        public static List<Department> GetDept()
        {
            var db = new ApplicationDbContext();
            return db.Departments.ToList();

        }

    }
}