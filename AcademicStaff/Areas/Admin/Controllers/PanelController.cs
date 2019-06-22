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

namespace AcademicStaff.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region
        // GET: Task/Panel
        public async Task<ActionResult> TaskIndex()
        {

            var tasks = await db.Tasks.Include(x => x.Report).Include(t => t.User).Include(x => x.Profile).OrderByDescending(x => x.DateCreated).ToListAsync();

            return View(tasks);
        }


        // GET: Task/Panel
        public async Task<ActionResult> ComTask()
        {

            var comtasks = await db.Reports.Include(x => x.Task).Include(t => t.User).Include(x => x.Profile).Where(x => x.TaskStatus == Models.Entities.TaskStatus.Completed).OrderByDescending(x => x.DateReported).ToListAsync();

            return View(comtasks);
        }


        // GET: Task/Panel
        public async Task<ActionResult> UndoneTask()
        {

            var untasks = await db.Tasks.Include(x => x.Report).Include(t => t.User).Include(x => x.Profile).Where(x => x.TaskStatus == Models.Entities.TaskStatus.Pending).OrderByDescending(x => x.DateCreated).ToListAsync();

            return View(untasks);
        }
        // GET: Task/Panel/Create
        public ActionResult CreateTask()
        {
            ViewBag.UserId = new SelectList(db.Profiles, "Id", "Fullname");
            return View();
        }

        // POST: Task/Panel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTask(Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.DateCreated = DateTime.Now.AddHours(1);
                tasks.TaskStatus = Models.Entities.TaskStatus.Pending;
                //tasks.ProfileId = ViewBag.UserId.Id;
                db.Tasks.Add(tasks);
                await db.SaveChangesAsync();
                TempData["success"] = "Task with the Title  " + tasks.TaskTitle + "  Added Successfully.";
                return RedirectToAction("TaskIndex");

            }

            ViewBag.UserId = new SelectList(db.Profiles, "Id", "Fullname", tasks.ProfileId);
            TempData["error"] = "Creation of new task not successful";
            return View(tasks);
        }

        // GET: Task/Panel/Edit/5
        public async Task<ActionResult> EditTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = await db.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Profiles, "Id", "Fullname", tasks.ProfileId);
            return View(tasks);
        }

        // POST: Task/Panel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTask(Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.DateCreated = DateTime.Now;
                db.Entry(tasks).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Task with the Title  "  + tasks.TaskTitle + "   Editted Successfully.";
                return RedirectToAction("TaskIndex");
            }
            ViewBag.UserId = new SelectList(db.Profiles, "Id", "Fullname", tasks.ProfileId);
            TempData["error"] = "Creation of new task not successful";
            return View(tasks);
        }


        // GET: Admin/Task/Details/5
        public async Task<ActionResult> TaskDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await db.Tasks.Include(x => x.User).Include(x => x.Report).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var item2 = await db.Reports.Include(x => x.User).Include(x => x.Task).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.task = item;
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: Task/Panel/Delete/5
        public async Task<ActionResult> DeleteTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = await db.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Task/Panel/Delete/5
        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tasks tasks = await db.Tasks.FindAsync(id);
            db.Tasks.Remove(tasks);
            await db.SaveChangesAsync();
            return RedirectToAction("TaskIndex");
        }

        #endregion

        #region
        // GET: Admin/Report
        public async Task<ActionResult> ReportIndex()
        {

            var report = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).OrderByDescending(x => x.DateReported).ToListAsync();


            return View(report);
        }


        public async Task<ActionResult> ReportDetail(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                TempData["success"] = "No Report Yet";
                return RedirectToAction("ReportIndex");
            }
            var item = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var item2 = await db.Tasks.Include(x => x.Report).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.task = item;
            if (item == null)
            {
                return HttpNotFound();

            }
            return View(item);
        }

        public async Task<ActionResult> AccessReport(int? id)
        {
            try
            {
                var item = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
                var task = db.Tasks.FirstOrDefault();
                var taskinfo = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
                ViewBag.task = taskinfo;
                return View(item);
            }
            catch (Exception e)
            {

                TempData["success"] = "No Report for this Task Yet";
                return RedirectToAction("ReportIndex");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AccessReport(Report model, int? id)
        {

            model.DateCommented = DateTime.UtcNow.AddHours(1);
           
          

            if (ModelState.IsValid)
            {
               
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();

                Tasks tasky = await db.Tasks.FindAsync(model.TaskId);
                tasky.TaskStatus = AcademicStaff.Models.Entities.TaskStatus.Completed;
                db.Entry(tasky).State = EntityState.Modified;
                await db.SaveChangesAsync();

                TempData["success"] = "Report Accessed Successfully";
                return RedirectToAction("ReportIndex");

            }
            var item = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            //var item = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var task = db.Tasks.FirstOrDefault();
            var taskinfo = await db.Reports.Include(x => x.User).Include(x => x.Task).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.task = taskinfo;
            TempData["error"] = "Unable to Save";
            return View(item);
        }




        // GET: Staff/DailyReport
        public async Task<ActionResult> DailyReportIndex()
        {
            var dailyReports = db.DailyReports.Include(d => d.Profile).Include(d => d.User).OrderByDescending(x => x.ReportDate);
            return View(await dailyReports.ToListAsync());

        }


        // GET: Staff/DailyReport/Details/5
        public async Task<ActionResult> DailyReportDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DailyReport dailyReport = await db.DailyReports.Include(x=>x.User).Include(x=>x.Profile).FindAsync(id);
            var item = await db.DailyReports.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        #endregion

        #region

        public async Task<ActionResult> DocIndex()
        {
            var doc = await db.Documents.Include(t => t.User).Include(x => x.Profile).Where(x => x.DocType == DocType.Official).OrderByDescending(x => x.DateUploaded).ToListAsync();

            return View(doc);
        }


        // GET: Staff/Documents/Details/5
        public async Task<ActionResult> SendReceiveDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x => x.Sender).Include(x => x.Receiver).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: Admin/Documents/Details/5
        public async Task<ActionResult> DocDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await db.Documents.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        public async Task<ActionResult> SendReceiveDocIndex()
        {
            var doc = await db.Documents.Include(t => t.User).Include(x => x.Profile).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.DocType2 == DocType2.SendandReceive).OrderByDescending(x => x.DateSent).ToListAsync();

            return View(doc);
        }
        // GET: Staff/Document/Edit/5
        public async Task<ActionResult> ReviewDoc(int? id)
        {

            var item = await db.Documents.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var docinfo = await db.Documents.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.doc = docinfo;
            return View(item);

        }

        // POST: Staff/Document/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReviewDoc(Document document, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Document Reviewed Successfully";
                return RedirectToAction("DocIndex");
            }
            var item = await db.Documents.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var docinfo = await db.Documents.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.doc = docinfo;
            TempData["error"] = "Unable to Review Document";
            return View(item);
        }

        #endregion

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
