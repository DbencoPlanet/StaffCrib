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

    public class TaskPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        public async Task<ActionResult> MyTasks()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var proid = db.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userid);
                var tasky = await db.Tasks.Include(x => x.User).Where(x => x.UserId == userid && x.ProfileId == proid.Id).ToListAsync();
                return View(tasky);
            }
            catch (Exception ex)
            {
                return RedirectToAction("TaskIndex");
            }

        }

        // GET: DailyReport/Panel
        public async Task<ActionResult> ReportIndex()
        {
            var userid = User.Identity.GetUserId();
            Profile profile = db.Profiles.FirstOrDefault(x => x.UserId == userid);
            ViewBag.profile = profile;
            Tasks task = db.Tasks.FirstOrDefault();
            ViewBag.task = task;
            var reports = db.Reports.Include(r => r.Task).Include(r => r.User);
            return View(await reports.ToListAsync());
        }

        // GET: DailyReport/Panel/Create
        public ActionResult CreateReport()
        {
            //var userid = User.Identity.GetUserId();
            ViewBag.TaskId = new SelectList(db.Tasks.Include(x => x.User), "Id", "TaskTitle");
            ViewBag.UserId = new SelectList(db.Profiles.Include(x => x.User), "Id", "Fullname");
            return View();
        }

        // POST: DailyReport/Panel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateReport([Bind(Include = "Id,UserId,ProfileId,TaskId,Content,Comment,DateReported,DateCommented")] Report report)
        {
            if (ModelState.IsValid)
            {

                db.Reports.Add(report);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //var userid = User.Identity.GetUserId();
            ViewBag.TaskId = new SelectList(db.Tasks.Include(x => x.User), "Id", "TaskTitle", report.TaskId);
            ViewBag.UserId = new SelectList(db.Profiles.Include(x => x.User), "Id", "Fullname", report.ProfileId);
            return View(report);
        }


        // GET: DailyReport/Panel/Edit/5
        public async Task<ActionResult> EditReport2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = await db.Reports.FindAsync(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            //var userid = User.Identity.GetUserId();
            ViewBag.TaskId = new SelectList(db.Tasks.Include(x => x.User), "Id", "TaskTitle", report.TaskId);
            ViewBag.UserId = new SelectList(db.Profiles.Include(x => x.User), "Id", "Fullname", report.ProfileId);
            return View(report);
        }

        // POST: DailyReport/Panel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReport2([Bind(Include = "Id,UserId,ProfileId,TaskId,Content,Comment,DateReported,DateCommented")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //var userid = User.Identity.GetUserId();
            ViewBag.TaskId = new SelectList(db.Tasks.Include(x => x.User), "Id", "TaskTitle", report.TaskId);
            ViewBag.UserId = new SelectList(db.Profiles.Include(x => x.User), "UserId", "Fullname", report.ProfileId);
            return View(report);
        }

        // GET: DailyReport/Panel/Delete/5
        public async Task<ActionResult> DeleteReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = await db.Reports.FindAsync(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: DailyReport/Panel/Delete/5
        [HttpPost, ActionName("DeleteReport")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Report report = await db.Reports.FindAsync(id);
            db.Reports.Remove(report);
            await db.SaveChangesAsync();
            return RedirectToAction("TaskIndex");
        }




        public async Task<ActionResult> StaffTask()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var proId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
                //Profile profile = db.Profiles.FirstOrDefault();
                //ViewBag.profile = profile;

                //var profileId = await db.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userid);
                var tasks = await db.Tasks.Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id).OrderByDescending(x=>x.DateCreated).Select(x => x.ProfileId).ToListAsync();

                var mytask = db.Tasks.Include(x => x.Report).Include(x => x.Profile).Where(x => tasks.Contains(x.ProfileId));

                return View(mytask);
                //var tasks = db.Tasks.Include(t => t.User).Include(x => x.Profile);

                //return View(await tasks.ToListAsync());
            }
            catch (Exception ex)
            {
                return RedirectToAction("TaskIndex");
            }

        }

        public async Task<ActionResult> ReportedTask()
        {
            var userid = User.Identity.GetUserId();
            var proId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
            var report = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id).OrderByDescending(x => x.DateReported).ToListAsync();
            //var item = await db.Tasks.Include(x => x.Report).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id && x.UserId == pId);

            //ViewBag.report = item;
            return View(report);
        }

        public async Task<ActionResult> TaskDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tasks = await db.Tasks.Include(x => x.User).Include(x => x.Report).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var report = await db.Reports.Include(x => x.User).Include(x => x.Task).Include(x => x.Profile).FirstOrDefaultAsync(x => x.TaskId == id);
            ViewBag.report = report;
            if (tasks == null)
            {
                return HttpNotFound();
            }
            var userid = User.Identity.GetUserId();
            var proId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
            var check = await db.Tasks.FirstOrDefaultAsync(x => x.Id == id && x.ProfileId == proId.Id);
            if (check != null)
            {
                if (tasks.EndDate > DateTime.UtcNow.Date)
                {
                    ViewBag.status = "<span class=\"\" style=\"font-weight:bolder;\">You Have Submited and can EDIT</span>";
                }
                else
                {
                    ViewBag.status = "<span class=\"text-success\" style=\"font-weight:bolder;\">You Have Submited and CANNOT EDIT </span>";
                }
            }
            else if(check != null)
            {
                if (report.TaskStatus == Models.Entities.TaskStatus.Completed)
                {
                    ViewBag.status = "<span class=\"text-warning\" style=\"font-weight:bolder;\">You Have Completed your task</span>";
                }
                else
                {
                    ViewBag.status = "<span class=\"text-danger\" style=\"font-weight:bolder;\">You Have Completed your task</span>";
                }
            }

            else
            {
                if (tasks.EndDate > DateTime.UtcNow.Date)
                {
                    ViewBag.status = "<span class=\"text-warning\" style=\"font-weight:bolder;\">You Have Not Submited</span>";
                }
                else
                {
                    ViewBag.status = "<span class=\"text-danger\" style=\"font-weight:bolder;\">You did not Submit your Task</span>";
                }
            }
            return View(tasks);
        }

        public async Task<ActionResult> ReportDetail(int? id)
        {


           
            var item = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.TaskId == id);
            if (item == null)
            {
                TempData["success"] = "No Report Yet";
                return RedirectToAction("StaffTask");

            }
          
            return View(item);
        }


        public async Task<ActionResult> SubmitReport(int? id)
        {
            var userid = User.Identity.GetUserId();
            var proId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
            var check = await db.Reports.FirstOrDefaultAsync(x => x.TaskId == id && x.UserId == userid);

            if (check != null)
            {
                return RedirectToAction("EditReport", new { id = check.Id});
            }

            var item = await db.Tasks.Include(x => x.User).Include(x => x.Report).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.task = item;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitReport(Report model, int? id)
        {


            var userid = User.Identity.GetUserId();
            var proId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
            var check = await db.Reports.FirstOrDefaultAsync(x => x.TaskId == id && x.UserId == userid);

            if (check != null)
            {
                return RedirectToAction("EditReport", new { id = model.Id });
            }


            if (ModelState.IsValid)
            {
                Tasks tasky = new Tasks();

                var taskstat = db.Tasks.FirstOrDefault(x => x.Id == id);
                var profileId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
                //var task = await db.Tasks.Include(x => x.Report).Include(x=>x.User).Include(x=>x.Profile).FirstOrDefaultAsync(x => x.Id == profileId.Id);
                model.TaskId = id;
                tasky.TaskStatus = taskstat.TaskStatus = AcademicStaff.Models.Entities.TaskStatus.Active;
                model.ProfileId = profileId.Id;
                model.TaskStatus = Models.Entities.TaskStatus.Active;
                model.UserId = userid;
                model.DateReported = DateTime.UtcNow.AddHours(1);
                db.Reports.Add(model);
                await db.SaveChangesAsync();
                TempData["success"] = "Report Submitted Successfully";
                return RedirectToAction("StaffTask");
            }
            TempData["error"] = "Unable to Save";
            return View(model);
        }

        public async Task<ActionResult> EditReport(int id)
        {
            var userid = User.Identity.GetUserId();
            var profileId = await db.Profiles.FirstOrDefaultAsync(x => x.UserId == userid);
            var item = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var taskinfo = await db.Reports.Include(x => x.User).Include(x => x.Task).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id || x.ProfileId == profileId.Id);
            ViewBag.task = taskinfo;
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReport(Report model, int id)
        {
           

            if (ModelState.IsValid)
            {

                //try
                //{
                //Tasks tasky = new Tasks();

                //var taskstat = db.Tasks.FirstOrDefault(x => x.Id == id);
                //var profileId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
                ////var task = await db.Tasks.Include(x => x.Report).Include(x=>x.User).Include(x=>x.Profile).FirstOrDefaultAsync(x => x.Id == profileId.Id);
                //model.TaskId = id;
                //tasky.TaskStatus = AcademicStaff.Models.Entities.TaskStatus.Active;
                //model.ProfileId = profileId.Id;
                //model.TaskStatus = Models.Entities.TaskStatus.Active;
                //model.UserId = userid;
                //model.DateReported = ViewBag.task.DateReported;
                //model.Content = content;
                model.DateModified = DateTime.UtcNow.AddHours(1);
                //model.Content = content;
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Report Edited Successfully";
                return RedirectToAction("StaffTask");
            }
            var userid = User.Identity.GetUserId();
            var proId = db.Profiles.FirstOrDefault(x => x.UserId == userid);
            var taskinfo = await db.Reports.Include(x => x.User).Include(x => x.Task).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id || x.ProfileId == proId.Id);
            ViewBag.task = taskinfo;

            TempData["error"] = "Unable to Update";
            return View(model);



            //  catch(Exception e)
            //{
            //    var userid = User.Identity.GetUserId();
            //    var profileId = await db.Profiles.FirstOrDefaultAsync(x => x.UserId == userid);
            //    var item = await db.Reports.Include(x => x.Task).Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            //    var taskinfo = await db.Reports.Include(x => x.User).Include(x => x.Task).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            //    ViewBag.task = taskinfo;
            //    TempData["error"] = "Unable to Save";
            //    return View(model);
            //}

            //}

            //var profileId = await db.Profiles.FirstOrDefaultAsync(x => x.UserId == userid);
            //var profileId = db.Profiles.FirstOrDefaultAsync();

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