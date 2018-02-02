using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrewDayAPP;

namespace BrewDayAPP.Controllers
{
    public class ShoppingListsController : Controller
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        // GET: ShoppingLists
        public async Task<ActionResult> Index()
        {
            var shoppingList = db.ShoppingList.Include(s => s.Ingredients);
            return View(await shoppingList.ToListAsync());
        }

        // GET: ShoppingLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = await db.ShoppingList.FindAsync(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            return View(shoppingList);
        }

        // GET: ShoppingLists/Create
        public ActionResult Create()
        {
            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description");
            return View();
        }

        // POST: ShoppingLists/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,IdIngredients,Quantity,UnitMeasure")] ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingList.Add(shoppingList);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description", shoppingList.IdIngredients);
            return View(shoppingList);
        }

        // GET: ShoppingLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = await db.ShoppingList.FindAsync(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description", shoppingList.IdIngredients);
            return View(shoppingList);
        }

        // POST: ShoppingLists/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,IdIngredients,Quantity,UnitMeasure")] ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdIngredients = new SelectList(db.Ingredients, "ID", "Description", shoppingList.IdIngredients);
            return View(shoppingList);
        }

        // GET: ShoppingLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = await db.ShoppingList.FindAsync(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            return View(shoppingList);
        }

        // POST: ShoppingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShoppingList shoppingList = await db.ShoppingList.FindAsync(id);
            db.ShoppingList.Remove(shoppingList);
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
