using AcademicStaff.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using AcademicStaff.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AcademicStaff.Models.Entities;

namespace AcademicStaff.Areas.Data.Services
{
    public class UserManagerService : IUserManagerService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public UserManagerService()
        {
        }

        public UserManagerService(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }



        

        public async Task<List<Profile>> AllUsers()
        {

            var users = db.Profiles.Include(x=>x.User).Where(x => x.User.UserName != "super@admin.com");
           
            return await users.OrderBy(x => x.Surname).ToListAsync();
        }

        public async Task<List<Profile>> MaleUsers()
        {

            var users = db.Profiles.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com" && x.Gender == Models.Entities.Gender.Male);

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }

        public async Task<List<Profile>> FemaleUsers()
        {

            var users = db.Profiles.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com" && x.Gender == Models.Entities.Gender.Female);

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }




        public async Task<bool> IsUsersinRole(string userid, string role)
        {
            var users = await _userManager.IsInRoleAsync(userid, role);
            return users;
        }
        ///
        //public int CountString(string searchString)
        //{
        //    int result = 0;

        //    searchString = searchString.Trim();

        //    if (searchString == "")
        //        return 0;

        //    while (searchString.Contains("  "))
        //        searchString = searchString.Replace("  ", " ");

        //    foreach (string y in searchString.Split(' '))

        //        result++;


        //    return result;
        //}


        public async Task<List<ApplicationUser>> Users()
        {
            return (await UserManager.Users.ToListAsync());
        }

        public async Task AddUserToRole(string userId, string rolename)
        {
            await UserManager.AddToRoleAsync(userId, rolename);
        }
        public async Task RemoveUserToRole(string userId, string rolename)
        {
            await UserManager.RemoveFromRoleAsync(userId, rolename);
        }

    }
}