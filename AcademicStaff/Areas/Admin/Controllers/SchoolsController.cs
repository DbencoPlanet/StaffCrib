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
using System.IO;

namespace AcademicStaff.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SchoolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Schools
        public async Task<ActionResult> Index()
        {
            return View(await db.Schools.OrderBy(x=>x.Name).ToListAsync());
        }

        // GET: Admin/Schools/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Admin/Schools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ShortCode")] School school, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                //    //picture
                if (picture != null && picture.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (picture.ContentLength > 0 && picture.ContentType.ToUpper().Contains("JPEG") || picture.ContentType.ToUpper().Contains("PNG") || picture.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(school.ShortCode + "_" + genNumber + "_" + picture.FileName);
                        school.Image = "~/Uploads/SchoolImage/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/SchoolImage/"), fileName);
                        picture.SaveAs(fileName);


                    }
                }

                db.Schools.Add(school);
                await db.SaveChangesAsync();
                TempData["success"] = "School with the Title  " + school.Name + "  Added Successfully.";
                return RedirectToAction("Index");
                
            }
            TempData["error"] = "Creation of new school not successful";
            return View(school);
        }

        // GET: Admin/Schools/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Admin/Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ShortCode")] School school, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                //    //picture
                if (picture != null && picture.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (picture.ContentLength > 0 && picture.ContentType.ToUpper().Contains("JPEG") || picture.ContentType.ToUpper().Contains("PNG") || picture.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(school.ShortCode + "_" + genNumber + "_" + picture.FileName);
                        school.Image = "~/Uploads/SchoolImage/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/SchoolImage/"), fileName);
                        picture.SaveAs(fileName);


                    }
                }

                db.Entry(school).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "School updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Unable to update school";
            return View(school);
        }

        // GET: Admin/Schools/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Admin/Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            School school = await db.Schools.FindAsync(id);
            db.Schools.Remove(school);
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
