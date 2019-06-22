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
    public class PublicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Staff/Publications
        public async Task<ActionResult> Index()
        {
            var userid = User.Identity.GetUserId();
            var profileId = await db.Profiles.FirstOrDefaultAsync(x => x.UserId == userid);
            var publications = await db.Publications.Include(p => p.Profile).Include(x=>x.User).Where(x=>x.ProfileId == profileId.Id).OrderByDescending(x=>x.DateCreated).ToListAsync();
            return View(publications);
        }

        // GET: Staff/Publications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Publication publication = await db.Publications.FindAsync(id);
            var publication = await db.Publications.Include(p => p.Profile).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            if (publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        // GET: Staff/Publications/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Staff/Publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Publication publication)
        {
            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                var profileId = await db.Profiles.FirstOrDefaultAsync(x => x.UserId == userid);
                publication.UserId = userid;
                publication.ProfileId = profileId.Id;
                publication.DateCreated = DateTime.UtcNow.AddHours(1);
                db.Publications.Add(publication);
                await db.SaveChangesAsync();
                TempData["success"] = "Publication with the Title  " + publication.Title + "  Added Successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Creation of new publication not successful";
            return View(publication);
        }

        // GET: Staff/Publications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = await db.Publications.FindAsync(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProfileId = new SelectList(db.Profiles, "Id", "UserId", publication.ProfileId);
            return View(publication);
        }

        // POST: Staff/Publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Publication publication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publication).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Publication edited Successfully.";
                return RedirectToAction("Index");
            }
            //ViewBag.ProfileId = new SelectList(db.Profiles, "Id", "UserId", publication.ProfileId);
            TempData["error"] = "Unable to edit publication";
            return View(publication);
        }

        // GET: Staff/Publications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = await db.Publications.FindAsync(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        // POST: Staff/Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Publication publication = await db.Publications.FindAsync(id);
            db.Publications.Remove(publication);
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
