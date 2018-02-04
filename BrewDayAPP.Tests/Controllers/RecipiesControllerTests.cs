using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;


namespace BrewDayAPP.Controllers.Tests
{
    [TestClass]
    public class RecipiesControllerTests
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        [TestMethod]
        public void controlla_Recipies_DetailsTest()
        {
            // Arrange
            RecipiesController controller = new RecipiesController();

            //Act
            //imposta il recupero della recipies id 2
            ActionResult result = controller.Details(2);

            // Assert
            //non mi aspetto, ad esempio come ritorno dopo la chiamata per id 2 recipies che description="black beer"
            //perchè sto lanciando senza utente loggto e non mi deve restituire 
            string Description = ((Recipies)((ViewResultBase)result).Model).Description;
            Assert.AreEqual("black beer", Description);
        }


        [TestMethod]
        public void controlla_Recipies_CreateTest()
        {

            // Arrange
            RecipiesController controller = new RecipiesController();

            //Act
            //rilancio la creazione della recpies che ha come descrizione "recipiesFotTestCreate" e utente SuperUser

            Recipies repciesFotTestCreate = new Recipies()
            {
                Description = "recipiesFotTestCreate",
                UserId= "1fe90eaa-4178-4b7f-8cb1-d38daaeadf95"
            };

            ActionResult result = controller.Create(repciesFotTestCreate);

            var idrepciesFotTestCreate = from s in db.Recipies
                                            .Where(x => x.Description.Equals("recipiesFotTestCreate"))
                                         select s.ID;
            // Assert
            //mi aspetto, che la selzione in base alla descrizione mi restituisca un ID
            Assert.IsNotNull(idrepciesFotTestCreate.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_Recipies_EditTest()
        {
            // Arrange
            RecipiesController controller = new RecipiesController();

            //Act
            //id della recipies che ha come descrizione "recipiesFotTestCreate"
            var idrecipiesBeforeEdit = from s in db.Recipies
                                            .Where(x => x.Description.Equals("recipiesFotTestCreate"))
                                       select s.ID;

            //creo recipies modificato Rate da 0 a 3
            Recipies recipiesFotTestEdit = new Recipies()
            {
                ID = idrecipiesBeforeEdit.FirstOrDefault(),
                Description = "recipiesFotTestCreate",
                UserId = "1fe90eaa-4178-4b7f-8cb1-d38daaeadf95",
                Rate = 3
            };


            //chiamo il controller per modificare
            ActionResult result = controller.Edit(recipiesFotTestEdit);

            //rileggo rate dopo edit
            var quantityAfterEdit = from s in db.Recipies
                                           .Where(x => x.Description.Equals("recipiesFotTestCreate"))
                                    select s.Rate;
            // Assert
            //mi che Rate=3
            Assert.AreEqual(3, quantityAfterEdit.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_Recipies_DeleteTest()
        {
            // Arrange
            RecipiesController controller = new RecipiesController();

            //Act
            //id dell'recipies che ha come descrizione "recipiesFotTestCreate"
            var idrecipiesBeforeDelete = from s in db.Recipies
                                            .Where(x => x.Description.Equals("recipiesFotTestCreate"))
                                         select s.ID;

            //chiamo il controller per cancellare
            ActionResult result = controller.DeleteConfirmed(idrecipiesBeforeDelete.FirstOrDefault());

            //rileggo se esiste l'id dopo la cancellazione
            var idrecipiesAfterDelete = from s in db.Recipies
                                           .Where(x => x.Description.Equals("recipiesFotTestCreate"))
                                        select s.ID;
            // Assert
            //mi aspetto che non ci sia l'id 0
            Assert.AreEqual(0, idrecipiesAfterDelete.FirstOrDefault());
        }
    }
}