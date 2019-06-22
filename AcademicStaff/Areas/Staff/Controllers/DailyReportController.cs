using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using Microsoft.AspNet.Identity;

namespace AcademicStaff.Areas.Staff.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Staff")]
    public class DailyReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Staff/DailyReport
        public async Task<ActionResult> Index()
        {
            var userid = User.Identity.GetUserId();

            var proId = await db.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userid);

            var daily = await  db.DailyReports.Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id && x.UserId == userid).OrderByDescending(x => x.ReportDate).Select(x => x.ProfileId).ToListAsync();

            var mydaily = db.DailyReports.Include(x => x.User).Where(x => daily.Contains(x.ProfileId));

            //var daily = await db.DailyReports.Include(x => x.Profile).Include(x => x.User).Where(x => x.UserId == userid && x.ProfileId == proId.Id).OrderByDescending(x => x.ReportDate).ToListAsync();

            return View(mydaily);


        }

        // GET: Staff/DailyReport/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dailyReport = await db.DailyReports.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            return View(dailyReport);
        }

        // GET: Staff/DailyReport/Create
        public ActionResult Create()
        {
            //var userid = User.Identity.GetUserId();
            //var proId = db.Profiles.FirstOrDefaultAsync(x=>x.UserId == userid);

            return View();
        }

        // POST: Staff/DailyReport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DailyReport dailyReport)
        {
            var userid = User.Identity.GetUserId();
            var proId = await db.Profiles.FirstOrDefaultAsync(x => x.UserId == userid);
            if (ModelState.IsValid)
            {
                dailyReport.DateCreated = DateTime.UtcNow.AddHours(1);
                dailyReport.ProfileId = proId.Id;
                dailyReport.UserId = userid;
                db.DailyReports.Add(dailyReport);
                await db.SaveChangesAsync();
                TempData["msg"] = "Report Submitted Successfully";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Unable to submit report";
            return View(dailyReport);
        }

        // GET: Staff/DailyReport/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = await db.DailyReports.FindAsync(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            return View(dailyReport);
        }

        // POST: Staff/DailyReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DailyReport dailyReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyReport).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["msg"] = "Report Editted Successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Unable to edit report";
            return View(dailyReport);
        }

        // GET: Staff/DailyReport/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = await db.DailyReports.FindAsync(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            return View(dailyReport);
        }

        // POST: Staff/DailyReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DailyReport dailyReport = await db.DailyReports.FindAsync(id);
            db.DailyReports.Remove(dailyReport);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
