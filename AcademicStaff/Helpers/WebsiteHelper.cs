using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicStaff.Helpers
{
    public class WebsiteHelper
    {

        public static List<School> Schools()
        {
            List<School> sch = new List<School>();
            using (var db = new ApplicationDbContext())
            {
                sch = db.Schools.OrderBy(x => x.ShortCode).ToList();

            }
            return sch;

        }


        public static List<Department> Depts(int cId)
        {
            List<Department> dept = new List<Department>();
            using (var db = new ApplicationDbContext())
            {
                dept = db.Departments.OrderBy(x => x.ShortCode).ToList();

            }
            return dept;

        }




        //public static School Schools()
        //{
        //    School schools = new School();
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var sch = db.Schools.OrderBy(x=>x.Name).ToList();
        //        //if (sch != null)
        //        //{
        //        //    schools = sch;
        //        //}


        //    }
        //    return schools;
        //}
    }
}