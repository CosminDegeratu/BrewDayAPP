using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;

namespace BrewDayAPP.Controllers.Tests
{
    [TestClass]
    public class IngredientsControllerTests
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        [TestMethod]
        public void coltrolla_Ingredients_IndexTest()
        {

            // Arrange
            IngredientsController controller = new IngredientsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void controlla_Ingredients_DetailsTest()
        {
            // Arrange
            IngredientsController controller = new IngredientsController();

            //Act
            //imposta il recupero dell'ingrediente id 1
            ActionResult result = controller.Details(1);

            // Assert
            //mi aspetto, ad esempio come ritorno dopo la chiamata per id 1 ingrediente che description=malts
            string Description = ((Ingredients)((ViewResultBase)result).Model).Description;
            Assert.AreEqual("malts", Description);
        }

        [TestMethod]
        public void controlla_Ingredients_CreateTest()
        {

            // Arrange
            IngredientsController controller = new IngredientsController();

            //Act
            //rilancio la creazione dell'ingrediente che ha come descrizione "ingredientFotTestCreate"

            Ingredients ingredientFotTestCreate = new Ingredients()
            {
                Description = "ingredientFotTestCreate",
                Quantity = 1000
            };

            ActionResult result = controller.Create(ingredientFotTestCreate);

            var idingredientFotTestCreate = from s in db.Ingredients
                                            .Where(x => x.Description.Equals("ingredientFotTestCreate"))
                                            select s.ID;
            // Assert
            //mi aspetto, che la selzione in base alla descrizione mi restituisca un ID
            Assert.IsNotNull(idingredientFotTestCreate.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_Ingredients_EditTest()
        {
            // Arrange
            IngredientsController controller = new IngredientsController();

            //Act
            //id dell'ingrediente che ha come descrizione "ingredientFotTestCreate"
            var idingredientBeforeEdit = from s in db.Ingredients
                                            .Where(x => x.Description.Equals("ingredientFotTestCreate"))
                                           select s.ID;

            //creo ingredients modificato quantty da 1000 a 2000 per passare all'edit In POST
            Ingredients ingredientFotTestEdit = new Ingredients()
            {
                ID= idingredientBeforeEdit.FirstOrDefault(),
                Description = "ingredientFotTestCreate",
                Quantity = 2000
            };


            //chiamo il controller per modificare
            ActionResult result = controller.Edit(ingredientFotTestEdit);

            //rileggo la quantità dopo edit
            var quantityAfterEdit = from s in db.Ingredients
                                           .Where(x => x.Description.Equals("ingredientFotTestCreate"))
                                          select s.Quantity;
            // Assert
            //mi che quantity=2000
            Assert.AreEqual(2000, quantityAfterEdit.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_Ingredients_DeleteTest()
        {
            // Arrange
            IngredientsController controller = new IngredientsController();

            //Act
            //id dell'ingrediente che ha come descrizione "ingredientFotTestCreate"
            var idingredientBeforeDelete = from s in db.Ingredients
                                            .Where(x => x.Description.Equals("ingredientFotTestCreate"))
                                            select s.ID;

            //chiamo il controller per cancellare
            ActionResult result = controller.DeleteConfirmed(idingredientBeforeDelete.FirstOrDefault());

            //rileggo se esiste l'id dopo la cancellazione
            var idingredientAfterDelete = from s in db.Ingredients
                                           .Where(x => x.Description.Equals("ingredientFotTestCreate"))
                                            select s.ID;
            // Assert
            //mi aspetto che non ci sia l'id0
            Assert.AreEqual(0,idingredientAfterDelete.FirstOrDefault());
        }
        
    }
}