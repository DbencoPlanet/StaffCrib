using AcademicStaff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using AcademicStaff.Models.Entities;
using System.Net;
using System.Threading.Tasks;
using AcademicStaff.Areas.Data.IServices;
using AcademicStaff.Areas.Data.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace AcademicStaff.Controllers
{
   

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IUserManagerService _userService = new UserManagerService();



        public HomeController()
        {

        }
        public HomeController(UserManagerService userService, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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




       


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _Slider()
        {
           

            var slider = db.ImageSlider.Where(x => x.CurrentSlider == true).ToList();
            return PartialView(slider);
        }

        public ActionResult _Gallery()
        {
            var slider = db.ImageGallery.Where(x => x.CurrentGallery == true).Take(9).ToList();
            return PartialView(slider);
        }

        public ActionResult _Events()
        {
            var events = db.Events.OrderByDescending(x=>x.Id).Take(3).ToList();
            return PartialView(events);
        }


        public ActionResult _Publication()
        {
            var pub = db.Publications.Include(x => x.User).Include(x => x.Profile).OrderByDescending(x => x.DateCreated).Take(3).ToList();
            return PartialView(pub);

        }



        //GET: ContactUs/Create
        public ActionResult ContactUs()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(string Subject, string Message, string PhoneNumber, string Email, string Name)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                ContactUs m = new ContactUs();
                m.Subject = Subject;
                m.Name = Name;
                m.Message = Message;
                m.PhoneNumber = PhoneNumber;
                m.Email = Email;
                m.DateCreated = DateTime.UtcNow.AddHours(1);
                db.ContactUs.Add(m);
                db.SaveChanges();
                TempData["msg"] = "Thanks for contacting us, we will get back to you shortly";

            }
            catch (Exception ex)
            {
                TempData["error"] = "Something Went Wrong. Try again.";
            }
            
            return RedirectToAction("Index", "Home");


        }


        public async Task<ActionResult> EventDetails(int? id)
        {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event events = await db.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }


        public ActionResult Events()
        {
          
        
            var post = db.Events.OrderByDescending(x=>x.Id).ToList();
            return View(post);

        }


        public ActionResult Gallery()
        {
           

            var gallery = db.ImageGallery.Where(x => x.CurrentGallery == true).ToList();
            return View(gallery);
        }


        // GET: Admin/Schools
        public ActionResult _School()
        {
         

                var sch = db.Schools.OrderBy(x=>x.Name).ToList();
                return PartialView(sch);
           
        }



        // GET: Admin/Dept
        public async Task<ActionResult> Dept(int? id)
        {


            var sch = await db.Departments.Include(x => x.School).Where(x=>x.SchoolId == id).ToListAsync();
            return View(sch);

        }


        // GET: Admin/Staff
        public async Task<ActionResult> Staff(int? id)
        {


            var sch = await db.Profiles.Include(x => x.School).Include(x=>x.Department).Include(x=>x.User).Where(x => x.DepartmentId == id).ToListAsync();
            return View(sch);

        }

        // GET: Admin/StaffInfo
        [AllowAnonymous]
        public ActionResult StaffInfo(string id)
        {
            var sch = db.Profiles.Include(x => x.User).Include(x => x.School).Include(x => x.Department).Include(x => x.Publication).FirstOrDefault(x=>x.UserId == id);
            ViewBag.sch = sch;
            var pub = db.Publications.Include(x => x.User).Include(x => x.Profile).Where(x => x.UserId == id).ToListAsync();
            ViewBag.pub = pub;
            return View(sch);

        }

      

        // GET: Admin/Staff
        public async Task<ActionResult> Publication()
        {
            var pub = await db.Publications.Include(x => x.User).Include(x => x.Profile).OrderByDescending(x=>x.DateCreated).Take(12).ToListAsync();
            return View(pub);

        }


       
        public async Task<ActionResult> PubDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication pub = await db.Publications.FirstOrDefaultAsync(x => x.Id == id);
            if (pub == null)
            {
                return HttpNotFound();
            }
            return View(pub);
        }

    }


   
}