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

namespace AcademicStaff.Areas.Staff.Controllers
{

    public class UserController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private IUserManagerService _userService = new UserManagerService();



        public UserController()
        {

        }
        public UserController(UserManagerService userService, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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


       

        [Authorize(Roles = "Admin,SuperAdmin,Staff")]
        public async Task<ActionResult> MyProfile()
        {
            var userinfo = User.Identity.GetUserId();
            var profile = await db.Profiles.Include(x => x.User).Include(x=>x.School).Include(x=>x.Department).FirstOrDefaultAsync(x => x.UserId == userinfo);
            ViewBag.profile = profile;
            return View();
        }


        // GET: Career/Staff/Details/5
        public async Task<ActionResult> welcome(string userid)
        {
            var profile = await db.Profiles.Include(x => x.User).Include(x => x.School).Include(x => x.Department).FirstOrDefaultAsync(x => x.UserId == userid);
            ViewBag.profile = profile;
            return View();
        }



        public ActionResult Register(string userid)
        {
            ViewBag.id = userid;
            var userinfo = User.Identity.GetUserId();
            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            ViewBag.School = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        // POST: Staff/Portal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Profile profile, HttpPostedFileBase picture, string userid)
        {


            // Fill in the missing pieces
            profile.CurrentAppDate = DateTime.UtcNow.AddHours(1);
           

           

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

                var userinfo = User.Identity.GetUserId();
                profile.UserId = userinfo;



                ApplicationUser user = await UserManager.FindByIdAsync(profile.UserId);
                if (user != null)
                {
                    user.Status = Models.Entities.Status.Active;
                    // UserManager.Update(user);
                    await UserManager.UpdateAsync(user);
                }


                db.Profiles.Add(profile);
                await db.SaveChangesAsync();


                var staffReg = await db.Profiles.FirstOrDefaultAsync(x => x.UserId == userinfo);
                string numberid = staffReg.Id.ToString("D6");
                staffReg.StaffNo = "STAFF/" + numberid;
                //staffReg.UserId = ViewBag.id;
                db.Entry(staffReg).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("welcome", new { userid = profile.UserId });
            }
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", profile.StateOfOrigin);
            ViewBag.School = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name", profile.SchoolId);
            return View(profile);
        }



        [Authorize(Roles = "Admin,SuperAdmin,Staff")]
        public async Task<ActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            ViewBag.School = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name");


            Profile data = await db.Profiles.FindAsync(id);
            if (data == null)
            {
                return HttpNotFound();
            }


            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(Profile profile, HttpPostedFileBase picture, int? id)
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
                return RedirectToAction("EditProfile", "User");
            }
            TempData["error"] = "Unable to Edit Profile.";
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", profile.StateOfOrigin);
            ViewBag.School = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name", profile.SchoolId);
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

       

     

        public JsonResult LgaList(string Id)
        {
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var local = from s in db.LocalGovs
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(local.ToArray(), "LGAName", "LGAName"), JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeptList(int? Id)
        {
            var schId = db.Schools.FirstOrDefault(x => x.Id == Id).Id;
            var local = from s in db.Departments
                       where s.SchoolId == schId
                       select s;

            return Json(new SelectList(local.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
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