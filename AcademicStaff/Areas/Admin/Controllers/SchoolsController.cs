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
        public async Task<ActionResult> Create(School school, HttpPostedFileBase pictures)
        {
            //    //picture
            if (pictures != null && pictures.ContentLength > 0)

            {
                System.Random randomInteger = new System.Random();
                int genNumber = randomInteger.Next(1000);

                if (pictures.ContentLength > 0 && pictures.ContentType.ToUpper().Contains("JPEG") || pictures.ContentType.ToUpper().Contains("PNG") || pictures.ContentType.ToUpper().Contains("JPG"))
                {

                    string fileName = Path.GetFileName(school.ShortCode + "_" + genNumber + "_" + pictures.FileName);
                    school.Image = "~/Uploads/SchoolImage/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Uploads/SchoolImage/"), fileName);
                    pictures.SaveAs(fileName);


                }
            }


            //if (school.ImageFile != null && school.ImageFile.ContentLength > 0)

            //{

            //    string fileName = Path.GetFileNameWithoutExtension(school.ImageFile.FileName);
            //    string extension = Path.GetExtension(school.ImageFile.FileName);
            //    {

            //    }
            //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //    school.Image = "~/Uploads/DeptImage/" + fileName;
            //    fileName = Path.Combine(Server.MapPath("~/Uploads/SchoolImage/"), fileName);
            //    school.ImageFile.SaveAs(fileName);
            //}
            //else
            //{
            //    TempData["error"] = "No File Selected";
            //    return RedirectToAction("Create");
            //}

            if (ModelState.IsValid)
            {
               
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
        public async Task<ActionResult> Edit(School school, HttpPostedFileBase pictures)
        {
            //    //picture
            if (pictures != null && pictures.ContentLength > 0)

            {
                System.Random randomInteger = new System.Random();
                int genNumber = randomInteger.Next(1000);

                if (pictures.ContentLength > 0 && pictures.ContentType.ToUpper().Contains("JPEG") || pictures.ContentType.ToUpper().Contains("PNG") || pictures.ContentType.ToUpper().Contains("JPG"))
                {

                    string fileName = Path.GetFileName(school.ShortCode + "_" + genNumber + "_" + pictures.FileName);
                    school.Image = "~/Uploads/SchoolImage/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Uploads/SchoolImage/"), fileName);
                    pictures.SaveAs(fileName);


                }
            }
            //if (school.ImageFile != null && school.ImageFile.ContentLength > 0)

            //{

            //    string fileName = Path.GetFileNameWithoutExtension(school.ImageFile.FileName);
            //    string extension = Path.GetExtension(school.ImageFile.FileName);
            //    {

            //    }
            //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //    school.Image = "~/Uploads/DeptImage/" + fileName;
            //    fileName = Path.Combine(Server.MapPath("~/Uploads/DeptImage/"), fileName);
            //    school.ImageFile.SaveAs(fileName);
            //}
            //else
            //{
            //    TempData["error"] = "No File Selected";
            //    return RedirectToAction("Create");
            //}

            if (ModelState.IsValid)
            {
               

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
