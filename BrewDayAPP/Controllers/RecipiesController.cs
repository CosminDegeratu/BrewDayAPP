using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrewDayAPP;

namespace BrewDayAPP.Controllers
{
    public class RecipiesController : Controller
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        // GET: Recipies
        public ActionResult Index()
        {
            var recipies = db.Recipies.Include(r => r.AspNetUsers);
            return View(recipies.ToList());
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
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
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
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", recipies.UserId);
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
