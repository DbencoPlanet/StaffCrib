using AcademicStaff.Models;
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
    public class PublicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Publications
        public async Task<ActionResult> Index()
        {
            var pub = db.Publications.Include(d => d.Profile).Include(x=>x.User);
            return View(await pub.ToListAsync());
        }

        // GET: Admin/Publications/Details/5
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