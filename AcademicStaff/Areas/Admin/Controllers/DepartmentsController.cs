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
    public class DepartmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Departments
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.School).OrderBy(x=>x.Name);
            return View(await departments.ToListAsync());
        }

        // GET: Admin/Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = await db.Departments.Include(x=>x.School).FirstOrDefaultAsync(x=>x.Id == id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);


        }

        // GET: Admin/Departments/Create
        public ActionResult Create()
        {
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ShortCode,SchoolId")] Department department, HttpPostedFileBase picture)
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

                        string fileName = Path.GetFileName(department.ShortCode + "_" + genNumber + "_" + picture.FileName);
                        department.Image = "~/Uploads/DeptImage/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/DeptImage/"), fileName);
                        picture.SaveAs(fileName);


                    }
                }

                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", department.SchoolId);
            return View(department);
        }

        // GET: Admin/Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", department.SchoolId);
            return View(department);
        }

        // POST: Admin/Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ShortCode,SchoolId")] Department department, HttpPostedFileBase picture)
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

                        string fileName = Path.GetFileName(department.ShortCode + "_" + genNumber + "_" + picture.FileName);
                        department.Image = "~/Uploads/DeptImage/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/DeptImage/"), fileName);
                        picture.SaveAs(fileName);


                    }
                }

                db.Entry(department).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", department.SchoolId);
            return View(department);
        }

        // GET: Admin/Departments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Admin/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            db.Departments.Remove(department);
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
