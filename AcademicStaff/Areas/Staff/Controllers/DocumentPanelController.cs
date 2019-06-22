using AcademicStaff.Models;
using AcademicStaff.Models.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademicStaff.Areas.Staff.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Staff")]
    public class DocumentPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public async Task<ActionResult> DocumentIndex()
        {

            try
            {
                var userid = User.Identity.GetUserId();
                var proId = db.Profiles.FirstOrDefault(x=>x.UserId ==userid);
                var doc = await db.Documents.Include(x => x.User).Include(x => x.Profile).Where(x => x.ProfileId == proId.Id || x.UserId == userid).OrderByDescending(x=>x.DateUploaded).Select(x => x.ProfileId).ToListAsync();

                var mydoc = db.Documents.Include(x => x.User).Include(x => x.Profile).Where(x => doc.Contains(x.ProfileId));
               
                return View(mydoc);
            }
            catch (Exception ex)
            {
                return RedirectToAction("DocumentUpoad");
            }


        }

        public async Task<ActionResult> SendIndex()
        {

            try
            {
                var userid = User.Identity.GetUserId();
                var proId = db.Profiles.FirstOrDefault(x=>x.UserId == userid);
                var doc = await db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x=>x.Sender).Include(x=>x.Receiver).Where(x =>x.SenderId == proId.Id ).OrderByDescending(x => x.DateSent).Select(x => x.SenderId).ToListAsync();

                var mydoc = db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x=>x.Sender).Include(x=>x.Receiver).Where(x => doc.Contains(x.SenderId));

                return View(mydoc);
            }
            catch (Exception ex)
            {
                return RedirectToAction("DocumentIndex");
            }


        }


        public async Task<ActionResult> ReceiveIndex()
        {

            try
            {
                var userid = User.Identity.GetUserId();
                var proId = db.Profiles.FirstOrDefault(x=>x.UserId == userid);
                var doc = await db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x=>x.Receiver).Include(x=>x.Sender).Where(x =>x.ReceiverId == proId.Id ).OrderByDescending(x => x.DateReceive).Select(x => x.ReceiverId).ToListAsync();

                var mydoc = db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x=>x.Receiver).Include(x=>x.Sender).Where(x => doc.Contains(x.ReceiverId));

                return View(mydoc);
            }
            catch (Exception ex)
            {
                return RedirectToAction("DocumentIndex");
            }


        }

        public ActionResult DocumentUpload()
        {
            var userid = User.Identity.GetUserId();
            var proId = db.Profiles.FirstOrDefault(x=>x.UserId == userid);
            Profile data = db.Profiles.FirstOrDefault(x => x.Id == proId.Id);

            ViewBag.docType = new SelectList("DocType", "DocType");

            return View();

        }

        // GET: Staff/DocumentPanel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DocumentUpload(Document document, HttpPostedFileBase docFile)
        {
            var userid = User.Identity.GetUserId();
            var data = db.Profiles.FirstOrDefault(x=>x.UserId == userid);
            if (document.DocFile != null && document.DocFile.ContentLength > 0)

            {

                string fileName = Path.GetFileNameWithoutExtension(document.DocFile.FileName);
                string extension = Path.GetExtension(document.DocFile.FileName);
                {

                }
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                document.DocUrl = "~/Uploads/Doc/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Uploads/Doc/"), fileName);
                document.DocFile.SaveAs(fileName);
            } else
            {
                TempData["error"] = "No File Selected";
                return RedirectToAction("DocumentUpload");
            }
        
            if (ModelState.IsValid)
            {
              

                document.UserId = userid;
                document.ProfileId = data.Id;
                document.DateUploaded = DateTime.UtcNow.AddHours(1);
                db.Documents.Add(document);
                await db.SaveChangesAsync();
                TempData["msg"] = "Uploaded Successfully";
                return RedirectToAction("DocumentIndex");
            }

          
            TempData["error"] = "Document was not uploadeded";
            return View(document);
        }


        // GET: Staff/Documents/Details/5
        public async Task<ActionResult> Details(int? id)
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


        // GET: Staff/Documents/Details/5
        public async Task<ActionResult> SendReceiveDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await db.Documents.Include(x => x.User).Include(x => x.Profile).Include(x=>x.Sender).Include(x=>x.Receiver).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }



        // GET: Staff/Document/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var userid = User.Identity.GetUserId();
            var profileId = await db.Profiles.FirstOrDefaultAsync(x=>x.UserId == userid);
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
        public async Task<ActionResult> Edit(Document document, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Report Edited Successfully";
                return RedirectToAction("DocumentIndex");
            }
            var userid = User.Identity.GetUserId();
            var profileId = await db.Profiles.FirstOrDefaultAsync(x=>x.UserId == userid);
            var item = await db.Documents.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            var docinfo = await db.Documents.Include(x => x.User).Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.doc = docinfo;
            TempData["error"] = "Unable to Save";
            return View(item);
        }


        // GET: Staff/Document/Edit/5
        public async Task<ActionResult> SendDoc(int? id)
        {
            var userid = User.Identity.GetUserId();
            var profileId = await db.Profiles.FirstOrDefaultAsync(x=>x.UserId == userid);
            var doc = await db.Documents.FirstOrDefaultAsync(x => x.ProfileId == profileId.Id && x.Id ==id);
            ViewBag.Receiver = new SelectList(db.Profiles, "Id", "Fullname");

            return View();

        }

        // POST: Staff/Document/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendDoc(Document document, int? id)
        {
            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                var profileId = await db.Profiles.FirstOrDefaultAsync(x=>x.UserId == userid);
                var doc = await db.Documents.FirstOrDefaultAsync(x => x.ProfileId == profileId.Id && x.Id == id);
                document.DateUploaded = doc.DateUploaded;
                document.Description = doc.Description;
                document.DocFile = doc.DocFile;
                document.DocName = doc.DocName;
                document.SenderId = profileId.Id;
                //document.DocType = doc.DocType;
                document.DocType2 = AcademicStaff.Models.Entities.DocType2.SendandReceive;
                document.DocUrl = doc.DocUrl;
                document.DateSent = DateTime.UtcNow.AddHours(1);
                document.DateReceive = DateTime.UtcNow.AddHours(1);

                db.Documents.Add(document);
                await db.SaveChangesAsync();
                TempData["success"] = "Document Sent Successfully";
                return RedirectToAction("SendIndex");
            }
            ViewBag.Receiver = new SelectList(db.Profiles, "Id", "Fullname",document.ReceiverId);
            TempData["error"] = "Unable to Save";
         
            return View();
        }


        // GET: Staff/Document/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Staff/Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Document document = await db.Documents.FindAsync(id);
            db.Documents.Remove(document);
            await db.SaveChangesAsync();
            return RedirectToAction("DocumentIndex");
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
    
