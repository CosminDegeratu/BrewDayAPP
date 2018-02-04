using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;
using BrewDayAPP.Controllers;
using System;

namespace BrewDayAPP.Tests.Controllers
{
    [TestClass]
    public class ShoppingListsControllerTests
    {

        private BrewDayDBEntities db = new BrewDayDBEntities();

        [TestMethod]
        public void coltrolla_ShoppingLists_IndexTest()
        {

            // Arrange
            ShoppingListsController controller = new ShoppingListsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void controlla_ShoppingLists_DetailsTest()
        {
            // Arrange
            ShoppingListsController controller = new ShoppingListsController();

            //Act
            //imposta il recupero della ShoppingList id 9
            ActionResult result = controller.Details(9);

            // Assert
            //mi aspetto, ad esempio come ritorno dopo la chiamata per id 1 ShoppingListe che description=malts
            string unitMeasure = ((ShoppingList)((ViewResultBase)result).Model).UnitMeasure;
            Assert.AreEqual("grams", unitMeasure);
        }

        [TestMethod]
        public void controlla_ShoppingLists_CreateTest()
        {

            // Arrange
            ShoppingListsController controller = new ShoppingListsController();

            //Act
            //rilancio la creazione dell'ShoppingListe che ha come descrizione "ShoppingListFotTestCreate"

            ShoppingList ShoppingListFotTestCreate = new ShoppingList()
            {
                IdIngredients=1,
                Quantity = 1000,
                UnitMeasure= "ShoppingListFotTestCreate"
            };

            ActionResult result = controller.Create(ShoppingListFotTestCreate);

            var idShoppingListFotTestCreate = from s in db.ShoppingList
                                            .Where(x => x.UnitMeasure.Equals("ShoppingListFotTestCreate"))
                                              select s.ID;
            // Assert
            //mi aspetto, che la selzione in base alla descrizione mi restituisca un ID
            Assert.IsNotNull(idShoppingListFotTestCreate.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_ShoppingLists_EditTest()
        {
            // Arrange
            ShoppingListsController controller = new ShoppingListsController();

            //Act
            //id dell'ShoppingListe che ha come descrizione "ShoppingListFotTestCreate"
            var idShoppingListBeforeEdit = from s in db.ShoppingList
                                            .Where(x => x.UnitMeasure.Equals("ShoppingListFotTestCreate"))
                                           select s.ID;

            //creo ShoppingLists modificato quantty da 1000 a 2000 per passare all'edit In POST
            ShoppingList ShoppingListFotTestEdit = new ShoppingList()
            {
                ID = idShoppingListBeforeEdit.FirstOrDefault(),
                UnitMeasure = "ShoppingListFotTestCreate",
                Quantity = 2000
            };


            //chiamo il controller per modificare
            ActionResult result = controller.Edit(ShoppingListFotTestEdit);

            //rileggo la quantità dopo edit
            var quantityAfterEdit = from s in db.ShoppingList
                                           .Where(x => x.UnitMeasure.Equals("ShoppingListFotTestCreate"))
                                    select s.Quantity;
            // Assert
            //mi che quantity=2000
            Assert.AreEqual(2000, quantityAfterEdit.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_ShoppingLists_DeleteTest()
        {
            // Arrange
            ShoppingListsController controller = new ShoppingListsController();

            //Act
            //id dell'ShoppingListe che ha come descrizione "ShoppingListFotTestCreate"
            var idShoppingListBeforeDelete = from s in db.ShoppingList
                                            .Where(x => x.UnitMeasure.Equals("ShoppingListFotTestCreate"))
                                             select s.ID;

            //chiamo il controller per cancellare
            ActionResult result = controller.DeleteConfirmed(idShoppingListBeforeDelete.FirstOrDefault());

            //rileggo se esiste l'id dopo la cancellazione
            var idShoppingListAfterDelete = from s in db.ShoppingList
                                           .Where(x => x.UnitMeasure.Equals("ShoppingListFotTestCreate"))
                                            select s.ID;
            // Assert
            //mi aspetto che non ci sia l'id0
            Assert.AreEqual(0, idShoppingListAfterDelete.FirstOrDefault());
        }
    }
}
