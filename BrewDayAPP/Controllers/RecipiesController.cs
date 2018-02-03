using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BrewDayAPP.Controllers
{
    public class RecipiesController : Controller
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        // GET: Recipies
        public ActionResult Index()
        {
            var recipies = db.Recipies.Include(r => r.AspNetUsers);
            var userID = User.Identity.GetUserId();
            if (User.Identity.GetUserName().Equals(ConfigurationManager.AppSettings["SuperUser"]))
            {
                //visualizza tutto
                return View(recipies.ToList());
            }
            else
            {   
                //filtra in base all'utente loggato
                return View(recipies.Where(x => x.UserId == userID).ToList());
            }

        }

        // GET: Recipies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recipies recipies = db.Recipies.Find(id);
            if (recipies == null)
            {
                return HttpNotFound();
            }
            return View(recipies);
        }

        // GET: Recipies/Create
        public ActionResult Create()
        {
            var userID = User.Identity.GetUserId();
            if (User.Identity.GetUserName().Equals(ConfigurationManager.AppSettings["SuperUser"]))
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            }
            else
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x => x.Id == userID), "Id", "Email");
            }
            return View();
        }

        // POST: Recipies/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Rate,UserId")] Recipies recipies)
        {
            if (ModelState.IsValid)
            {
                db.Recipies.Add(recipies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", recipies.UserId);
            return View(recipies);
        }

        // GET: Recipies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipies recipies = db.Recipies.Find(id);
            if (recipies == null)
            {
                return HttpNotFound();
            }
            var userID = User.Identity.GetUserId();
            if (User.Identity.GetUserName().Equals(ConfigurationManager.AppSettings["SuperUser"]))
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", recipies.UserId);
            }
            else
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x => x.Id == userID), "Id", "Email", recipies.UserId);
            }
            return View(recipies);
        }

        // POST: Recipies/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Rate,UserId")] Recipies recipies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", recipies.UserId);
            return View(recipies);
        }

        // GET: Recipies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipies recipies = db.Recipies.Find(id);
            if (recipies == null)
            {
                return HttpNotFound();
            }
            return View(recipies);
        }

        // POST: Recipies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipies recipies = db.Recipies.Find(id);
            db.Recipies.Remove(recipies);
            db.SaveChanges();
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
