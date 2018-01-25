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
    public class IngredientRecipesController : Controller
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        // GET: IngredientRecipes con parametro di ingresso id della ricetta
        public ActionResult Index(int? recipiesID)
        {
            var ingredientRecipe=db.IngredientRecipe.Include(i => i.Recipies);
            return View(ingredientRecipe.Where(x=>x.IdRecipes==recipiesID));
        }

        // GET: IngredientRecipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientRecipe ingredientRecipe = db.IngredientRecipe.Find(id);
            if (ingredientRecipe == null)
            {
                return HttpNotFound();
            }
            return View(ingredientRecipe);
        }

        // GET: IngredientRecipes/Create
        public ActionResult Create(int? recipiesID)
        {
            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description");
            ViewBag.IdRecipes = new SelectList(db.Recipies.Where(x=>x.ID==recipiesID), "ID", "Description");
            return View();
        }

        // POST: IngredientRecipes/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IdRecipes,IdIngredients,AbsolutQuantity,AbsolutUnitMeasure")] IngredientRecipe ingredientRecipe)
        {
            if (ModelState.IsValid)
            {
                db.IngredientRecipe.Add(ingredientRecipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description", ingredientRecipe.IdIngredients);
            ViewBag.IdRecipes = new SelectList(db.Recipies, "ID", "Description", ingredientRecipe.IdRecipes);
            return View(ingredientRecipe);
        }

        // GET: IngredientRecipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientRecipe ingredientRecipe = db.IngredientRecipe.Find(id);
            if (ingredientRecipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description", ingredientRecipe.IdIngredients);
            ViewBag.IdRecipes = new SelectList(db.Recipies, "ID", "Description", ingredientRecipe.IdRecipes);
            return View(ingredientRecipe);
        }

        // POST: IngredientRecipes/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IdRecipes,IdIngredients,AbsolutQuantity,AbsolutUnitMeasure")] IngredientRecipe ingredientRecipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredientRecipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description", ingredientRecipe.IdIngredients);
            ViewBag.IdRecipes = new SelectList(db.Recipies, "ID", "Description", ingredientRecipe.IdRecipes);
            return View(ingredientRecipe);
        }

        // GET: IngredientRecipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientRecipe ingredientRecipe = db.IngredientRecipe.Find(id);
            if (ingredientRecipe == null)
            {
                return HttpNotFound();
            }
            return View(ingredientRecipe);
        }

        // POST: IngredientRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IngredientRecipe ingredientRecipe = db.IngredientRecipe.Find(id);
            db.IngredientRecipe.Remove(ingredientRecipe);
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
