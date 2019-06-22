using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace AcademicStaff.Areas.Staff.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Staff")]
    public class DashBoardController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Staff/DashBoard
        public ActionResult Index()
        {

            //Get My Task Count
            var userid = User.Identity.GetUserId();
            var proId = db.Profiles.FirstOrDefault(x=>x.UserId == userid);
            var tasks = db.Tasks.Include(x => x.User).Include(x => x.Profile).Include(x => x.Report).Where(x => x.ProfileId == proId.Id).Count();
            ViewBag.tasks = tasks;


            //Get My Report Count

            
            var report = db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id && x.UserId ==userid).Count();
            ViewBag.report = report;



            //Get All Document Count

            var doc = db.Documents.Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id || x.UserId == userid).Count();
            ViewBag.doc = doc;

            //Get Sent Document Count
            var sentdoc = db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.SenderId == proId.Id).Count();
            ViewBag.sentdoc = sentdoc;


            //Get receive Document Coount
            var recdoc = db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.ReceiverId == proId.Id).Count();
            ViewBag.recdoc = recdoc;


        

            //Get My Staff Daily Report Count
          
            var dreport = db.DailyReports.Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id && x.UserId == userid).Count();
            ViewBag.dreport = dreport;

            var mypub = db.Publications.Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id && x.UserId == userid).Count();
            ViewBag.mypub = mypub;

            return View();
        }

        public JsonResult GetEvents()
        {
            var userid = User.Identity.GetUserId();
            var events = db.Events.Where(x => x.UserId == userid || x.GeneralEvent == true).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

       
        }
    }
