using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrewDayAPP;
using Microsoft.AspNet.Identity;

namespace BrewDayAPP.Controllers
{
    public class BrewsController : Controller
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        // GET: Brews
        public ActionResult Index()
        {
            var brews = db.Brews.Include(b => b.AspNetUsers).Include(b => b.Recipies);
            var userID = User.Identity.GetUserId();
            if (User.Identity.GetUserName().Equals(ConfigurationManager.AppSettings["SuperUser"]))
            {
                return View(brews.ToList());
            }
            else
            {
                return View(brews.Where(x=>x.UserId==userID).ToList());
            }
                
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
            var userID = User.Identity.GetUserId();
            if (User.Identity.GetUserName().Equals(ConfigurationManager.AppSettings["SuperUser"]))
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
                ViewBag.IdRecipies = new SelectList(db.Recipies, "ID", "Description");
            }
            else
            {
                ViewBag.UserId = new SelectList(db.AspNetUsers.Where(x=>x.Id==userID), "Id", "Email");
                ViewBag.IdRecipies = new SelectList(db.Recipies.Where(x=>x.UserId==userID), "ID", "Description");
            }
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
                //recupera dal model parametri che mi servono per la spUpdateQuantityIngredients
                var recipiesId = brews.IdRecipies;
                var batchSize = brews.BatchSize;

                //lancia la spUpdateQuantityIngredients
                var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                var command = new SqlCommand("spUpdateQuantityIngredients", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@recipiesId", recipiesId);
                command.Parameters.AddWithValue("@batchSize", batchSize);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                //lancia inserimento di un nuovo record per Brews
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
