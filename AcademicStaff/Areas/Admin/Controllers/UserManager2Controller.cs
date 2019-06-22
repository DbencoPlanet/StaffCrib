using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using AcademicStaff.Areas.Data.IServices;
using AcademicStaff.Areas.Data.Services;
using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;

namespace AcademicStaff.Areas.Admin.Controllers
{
   
    public class UserManager2Controller : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private IUserManagerService _userService = new UserManagerService();
        


        public UserManager2Controller()
        {

        }
        public UserManager2Controller(UserManagerService userService, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userService = userService;
            UserManager = userManager;
            RoleManager = roleManager;
           
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        // GET: Admin/UserManagers
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Index()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();
           
            var items = await _userService.AllUsers();
            ViewBag.countall = items.Count();
           
            return View(items);

        }

        [Authorize(Roles = "Admin,SuperAdmin,Staff")]
        public async Task<ActionResult> MyProfile()
        {
            var userinfo = User.Identity.GetUserId();
            var profile = await db.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userinfo);
            ViewBag.profile = profile;
            return View();
        }



        public ActionResult Register(int? userid)
        {
            ViewBag.id = userid;
            return View();
        }

        // POST: Staff/Portal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Profile profile, HttpPostedFileBase picture, int? userid)
        {

            ViewBag.id = userid;
            // Fill in the missing pieces
            profile.CurrentAppDate = DateTime.UtcNow.AddHours(1);
            profile.UserId = ViewBag.id;


            if (ModelState.IsValid)
            {
                //    //picture
                if (picture != null && picture.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (picture.ContentLength > 0 && picture.ContentType.ToUpper().Contains("JPEG") || picture.ContentType.ToUpper().Contains("PNG") || picture.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(profile.Surname + "_" + profile.FirstName + "_" + genNumber + "_" + picture.FileName);
                        profile.Photo = "~/Uploads/Profile/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Profile/"), fileName);
                        picture.SaveAs(fileName);


                    }
                }



                db.Profiles.Add(profile);
                await db.SaveChangesAsync();
                return RedirectToAction("welcome", new { userid = profile.Id });
            }

            return View(profile);
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> UserProfile(int? id)
        {
            var profile = await db.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.profile = profile;
            return View();
        }

        [Authorize(Roles = "Admin,SuperAdmin,Staff")]
        public async Task<ActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile data = await db.Profiles.FindAsync(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(Profile profile, HttpPostedFileBase picture,int? id)
        {
            //Profile data = await db.Profiles.FindAsync(id);
            // Create new Article object


            if (ModelState.IsValid)
            {
                //    //picture
                if (picture != null && picture.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (picture.ContentLength > 0 && picture.ContentType.ToUpper().Contains("JPEG") || picture.ContentType.ToUpper().Contains("PNG") || picture.ContentType.ToUpper().Contains("JPG"))
                    {

                        //WebImage img = new WebImage(picture.InputStream);
                        string fileName = Path.GetFileName(profile.Surname + "_" + profile.FirstName + "_" + genNumber + "_" + picture.FileName);
                        profile.Photo = "~/Uploads/Profile/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Profile/"), fileName);
                        //img.Save(fileName);
                        // file.SaveAs(fileName);
                        picture.SaveAs(fileName);
                        


                    }
                }



                db.Entry(profile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Profile Edited Successfully.";
                return RedirectToAction("EditProfile", "UserManager");
            }
            TempData["error"] = "Unable to Edit Profile.";
            return View(profile);
        }





        // GET: Task/Panel/Edit/5
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> EditUserProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile data = await db.Profiles.FindAsync(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserProfile(Profile profile, HttpPostedFileBase picture,int? id)
        {
            //Profile data = await db.Profiles.FindAsync(id);
            // Create new Article object


            if (ModelState.IsValid)
            {
                //    //picture
                if (picture != null && picture.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (picture.ContentLength > 0 && picture.ContentType.ToUpper().Contains("JPEG") || picture.ContentType.ToUpper().Contains("PNG") || picture.ContentType.ToUpper().Contains("JPG"))
                    {

                       
                        string fileName = Path.GetFileName(profile.Surname + "_" + profile.FirstName + "_" + genNumber + "_" + picture.FileName);
                        profile.Photo = "~/Uploads/Profile/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Profile/"), fileName);
                        picture.SaveAs(fileName);


                    }
                }




                db.Entry(profile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Profile Edited Successfully.";
                return RedirectToAction("EditUserProfile", "UserManager");
            }
            TempData["error"] = "Unable to Edit Profile.";
            return View(profile);
        }




        [HttpPost]
        public async Task<ActionResult> UserToRole(string rolename, string userId, bool? ischecked)
        {
            if (ischecked.HasValue && ischecked.Value)
            {
                await _userService.AddUserToRole(userId, rolename);
            }
            else
            {
                await _userService.RemoveUserToRole(userId, rolename);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Roles/
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Roles()
        {
            return View(RoleManager.Roles.Where(x => x.Name != "SuperAdmin"));
        }

        //
        // GET: /Roles/Details/5
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> RoleDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            var usersitem = await _userService.Users();
            foreach (var user in usersitem)
            {
                if (await _userService.IsUsersinRole(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //
        // GET: /Roles/Create
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult CreateRole()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(roleViewModel.Name);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Roles");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Roles");
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Roles");
            }
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}