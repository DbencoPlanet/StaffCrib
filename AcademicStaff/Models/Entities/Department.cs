using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public string Image { get; set; }


        public int? SchoolId { get; set; }
        public School School { get; set; }


        public static List<Department> GetDept()
        {
            var db = new ApplicationDbContext();
            return db.Departments.ToList();

        }

    }
}