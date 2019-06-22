using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;


namespace AcademicStaff.Areas.Service
{
    public class GeneralService
    {
        public static bool IsUserInRole(string userId, string role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (manager.IsInRole(userId, role))
            {
                return true;
            }

            return false;
        }

        public static Profile UserInfo()
        {
            Profile user;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                user = db.Profiles.Include(x => x.User).FirstOrDefault(x => x.UserId == userid);
               
            }
            return user;
            
        }
    }
}