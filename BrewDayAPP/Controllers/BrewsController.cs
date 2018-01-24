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
    public class BrewsController : Controller
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        // GET: Brews
        public ActionResult Index()
        {
            var brews = db.Brews.Include(b => b.AspNetUsers).Include(b => b.Recipies);
            return View(brews.ToList());
        }

        // GET: Brews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brews brews = db.Brews.Find(id);
            if (brews == null)
            {
                return HttpNotFound();
            }
            return View(brews);
        }

        // GET: Brews/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdRecipies = new SelectList(db.Recipies, "ID", "Description");
            return View();
        }

        // POST: Brews/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,IdRecipies,BatchSize,Notes,DateBrew,UserId")] Brews brews)
        {
            if (ModelState.IsValid)
            {
                db.Brews.Add(brews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", brews.UserId);
            ViewBag.IdRecipies = new SelectList(db.Recipies, "ID", "Description", brews.IdRecipies);
            return View(brews);
        }

        // GET: Brews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brews brews = db.Brews.Find(id);
            if (brews == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", brews.UserId);
            ViewBag.IdRecipies = new SelectList(db.Recipies, "ID", "Description", brews.IdRecipies);
            return View(brews);
        }

        // POST: Brews/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,IdRecipies,BatchSize,Notes,DateBrew,UserId")] Brews brews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", brews.UserId);
            ViewBag.IdRecipies = new SelectList(db.Recipies, "ID", "Description", brews.IdRecipies);
            return View(brews);
        }

        // GET: Brews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brews brews = db.Brews.Find(id);
            if (brews == null)
            {
                return HttpNotFound();
            }
            return View(brews);
        }

        // POST: Brews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brews brews = db.Brews.Find(id);
            db.Brews.Remove(brews);
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
