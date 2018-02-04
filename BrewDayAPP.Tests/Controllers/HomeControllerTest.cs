using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrewDayAPP;
using BrewDayAPP.Controllers;

namespace BrewDayAPP.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        //questi test si limitano a controllare se la view viene restituita come view result alla chiamata di un metodo del controller.
        //mettendo in HomeController Nella ViewBag il nome della pagina, controllo se il metodo restituisce il nome della pagina nella view alla sua invocazione
        [TestMethod]
        public void controlla_ritorno_della_view_per_Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void controlla_ritorno_della_view_per_Ingredients()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Ingredients() as ViewResult;

            // Assert
            Assert.AreEqual("Ingredients page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void controlla_ritorno_della_view_per_Recipies()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Recipies() as ViewResult;

            // Assert
            Assert.AreEqual("Recipies page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void controlla_ritorno_della_view_per_IngredientRecipes()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.IngredientRecipes() as ViewResult;

            // Assert
            Assert.AreEqual("IngredientRecipes page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void controlla_ritorno_della_view_per_Brews()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Brews() as ViewResult;

            // Assert
            Assert.AreEqual("Brews page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void controlla_ritorno_della_view_per_RecipiesOfTheDay()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.RecipiesOfTheDay() as ViewResult;

            // Assert
            Assert.AreEqual("RecipiesOfTheDay page.", result.ViewBag.Message);
        }


        [TestMethod]
        public void controlla_ritorno_della_view_per_IngredientsToSubstract()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.IngredientsToSubstract() as ViewResult;

            // Assert
            Assert.AreEqual("IngredientsToSubstract page.", result.ViewBag.Message);
        }


        [TestMethod]
        public void controlla_ritorno_della_view_per_ShoppingLists()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ShoppingLists() as ViewResult;

            // Assert
            Assert.AreEqual("ShoppingLists page.", result.ViewBag.Message);
        }

    }
}
