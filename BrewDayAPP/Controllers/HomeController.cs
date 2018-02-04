using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrewDayAPP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ingredients()
        {
            ViewBag.Message = "Ingredients page.";

            return View();
        }

        public ActionResult Recipies()
        {
            ViewBag.Message = "Recipies page.";

            return View();
        }

        public ActionResult IngredientRecipes()
        {
            ViewBag.Message = "IngredientRecipes page.";

            return View();
        }

        public ActionResult Brews()
        {
            ViewBag.Message = "Brews page.";

            return View();
        }

        public ActionResult RecipiesOfTheDay()
        {
            ViewBag.Message = "RecipiesOfTheDay page.";

            return View();
        }

        public ActionResult IngredientsToSubstract()
        {
            ViewBag.Message = "IngredientsToSubstract page.";

            return View();
        }

        public ActionResult ShoppingLists()
        {
            ViewBag.Message = "ShoppingLists page.";

            return View();
        }
    }
}