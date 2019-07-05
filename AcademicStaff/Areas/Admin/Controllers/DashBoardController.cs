using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicStaff.Areas.Service;
using System.IO;

namespace AcademicStaff.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DashBoardController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            //Get Task Count
            var task = db.Tasks.Count();
            ViewBag.task = task;

            //Get Undone Task Count
            var uncomtask = db.Tasks.Where(z => z.TaskStatus == TaskStatus.Pending).Count();
            ViewBag.untask = uncomtask;

            //Get completed task Task Count
            var comtask = db.Reports.Where(z => z.TaskStatus == TaskStatus.Completed).Count();
            ViewBag.comtask = comtask;

            //Get My Staff Count
            var staff = db.Profiles.Where(x => x.User.Email != "super@admin.com").Count();

            ViewBag.staff = staff;

            //Get My Staff Count
            var mstaff = db.Profiles.Where(x=>x.Gender == Gender.Male && x.User.Email != "super@admin.com").Count();
            ViewBag.mstaff = mstaff;

            //Get My Staff Count
            var fstaff = db.Profiles.Where(x=>x.Gender == Gender.Female && x.User.Email != "super@admin.com").Count();
            ViewBag.fstaff = fstaff;

            //Get My School Count
            var sch = db.Schools.Count();
            ViewBag.sch = sch;

            //Get My Department Count
            var dept = db.Departments.Count();
            ViewBag.dept = dept;

         

            //Get My Staff Task Report Count
            var report = db.Reports.Count();
            ViewBag.report = report;

            //Get My Staff Daily Report Count
            var dreport = db.DailyReports.Count();
            ViewBag.dreport = dreport;



            //Get Document Count
            var doc = db.Documents.Where(x => x.DocType == Models.Entities.DocType.Official).Count();
            ViewBag.doc = doc;

            //Get Send and Receive Document Count
            var sentandrecdoc = db.Documents.Where(x => x.DocType2 == Models.Entities.DocType2.SendandReceive).Count();
            ViewBag.sentandrecdoc = sentandrecdoc;


            //Get Personal Document Count
            var pdoc = db.Documents.Where(x => x.DocType == Models.Entities.DocType.Personal).Count();
            ViewBag.pdoc = pdoc;





            return View();
        }

        public JsonResult GetEvents()
        {
            var userid = User.Identity.GetUserId();
            var events = db.Events.Where(x => x.UserId == userid || x.GeneralEvent == true).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvent(string subject, string description, DateTime start, DateTime end, string color, bool general, bool fday, HttpPostedFileBase pictures)
        {
            try
            {
                string u = User.Identity.GetUserId();
                Event m = new Event();
                //    //picture
                if (pictures != null && pictures.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (pictures.ContentLength > 0 && pictures.ContentType.ToUpper().Contains("JPEG") || pictures.ContentType.ToUpper().Contains("PNG") || pictures.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(m.Id + "_" + genNumber + "_" + pictures.FileName);
                        m.Image = "~/Uploads/Events/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Events/"), fileName);
                        pictures.SaveAs(fileName);


                    }
                }
                m.Subject = subject;
                m.DIscription = description;
                m.Start = start;
                m.End = end;
                m.Color = color;
                m.GeneralEvent = general;
                m.IsFullDay = fday;
                m.UserId = u;
                db.Events.Add(m);
                db.SaveChanges();
                TempData["success"] = "Event Added";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Something Went Wrong. Try again.";
            }
            return RedirectToAction("Index");
        }
    }
}