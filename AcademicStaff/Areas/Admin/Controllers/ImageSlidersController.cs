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
using AcademicStaff.Areas.Data.IServices;
using AcademicStaff.Areas.Data.Services;

namespace AcademicStaff.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ImageSlidersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IimageSliderServices _ImageSliderServices = new ImageSliderServices();

        public ImageSlidersController()
        { }
        public ImageSlidersController(ImageSliderServices imageServices)
        {
            _ImageSliderServices = imageServices;
        }

        // GET: Admin/ImageSliders
        public async Task<ActionResult> Index()
        {
            var imageSlider = await _ImageSliderServices.List();
            return View(imageSlider);
        }

        // GET: Admin/ImageSliders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSlider imageSlider = await db.ImageSlider.FindAsync(id);
            if (imageSlider == null)
            {
                return HttpNotFound();
            }

            return View(imageSlider);
        }

        // GET: Admin/ImageSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ImageSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImageSlider models, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                models.CurrentSlider = true;
                await _ImageSliderServices.New(models, upload);
                return RedirectToAction("Index");
            }

            return View(models);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSlider img = await db.ImageSlider.FindAsync(id);
            if (img == null)
            {
                return HttpNotFound();
            }
           return View(img);
        }

        // POST: Content/Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ImageSlider imageSlider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageSlider).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           return View(imageSlider);
        }


        // POST: Admin/ImageSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToSlider(int id)
        {
            var models = await db.ImageSlider.FindAsync(id);
                await _ImageSliderServices.AddToSlider(models);
                return RedirectToAction("Index");
              
        }

        // GET: Admin/ImageSliders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSlider imageSlider = await db.ImageSlider.FindAsync(id);
            if (imageSlider == null)
            {
                return HttpNotFound();
            }
            return View(imageSlider);
        }

        // POST: Admin/ImageSliders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _ImageSliderServices.Delete(id);
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
