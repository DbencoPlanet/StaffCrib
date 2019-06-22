using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicStaff.Areas.Data.IServices
{
    interface IUserManagerService
    {
       
        Task<bool> IsUsersinRole(string userid, string role);
        Task AddUserToRole(string userId, string rolename);
        Task RemoveUserToRole(string userId, string rolename);
        Task<List<ApplicationUser>> Users();
        Task<List<Profile>> AllUsers();
        Task<List<Profile>> MaleUsers();
        Task<List<Profile>> FemaleUsers();
    }
}
