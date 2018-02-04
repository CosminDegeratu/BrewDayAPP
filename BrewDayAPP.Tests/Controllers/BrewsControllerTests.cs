using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;


namespace BrewDayAPP.Controllers.Tests
{
    [TestClass]
    public class BrewsControllerTests
    {
        private BrewDayDBEntities db = new BrewDayDBEntities();

        [TestMethod]
        public void controlla_Brews_DetailsTest()
        {
            // Arrange
            BrewsController controller = new BrewsController();

            //Act
            //imposta il recupero della brews id 2
            ActionResult result = controller.Details(2);

            // Assert
            //mi aspetto, ad esempio come ritorno dopo la chiamata per id 2 brews che description="Brew of blonde beer" 
            string Description = ((Brews)((ViewResultBase)result).Model).Description;
            Assert.AreEqual("Brew of blonde beer", Description);
        }


        [TestMethod]
        public void controlla_Brews_CreateTest()
        {

            // Arrange
            BrewsController controller = new BrewsController();

            //Act
            //rilancio la creazione della brews che ha come descrizione "brewsFotTestCreate" e utente SuperUser

            Brews brewsFotTestCreate = new Brews()
            {
                Description = "brewsFotTestCreate",
                IdRecipies=1,
                BatchSize=1,
                UserId= "1fe90eaa-4178-4b7f-8cb1-d38daaeadf95"
            };

            ActionResult result = controller.Create(brewsFotTestCreate);

            var idrepciesFotTestCreate = from s in db.Brews
                                            .Where(x => x.Description.Equals("brewsFotTestCreate"))
                                         select s.ID;
            // Assert
            //mi aspetto, che la selzione in base alla descrizione mi restituisca un ID
            Assert.IsNotNull(idrepciesFotTestCreate.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_Brews_EditTest()
        {
            // Arrange
            BrewsController controller = new BrewsController();

            //Act
            //id della brews che ha come descrizione "brewsFotTestCreate"
            var idbrewsBeforeEdit = from s in db.Brews
                                            .Where(x => x.Description.Equals("brewsFotTestCreate"))
                                    select s.ID;

            //modifico il campo Notes
            Brews brewsFotTestEdit = new Brews()
            {
                ID = idbrewsBeforeEdit.FirstOrDefault(),
                Description = "brewsFotTestCreate",
                BatchSize=1,
                UserId = "1fe90eaa-4178-4b7f-8cb1-d38daaeadf95",
                Notes = "notes for brews"
            };


            //chiamo il controller per modificare
            ActionResult result = controller.Edit(brewsFotTestEdit);

            //rileggo Notes dopo edit
            var notesAfterEdit = from s in db.Brews
                                           .Where(x => x.Description.Equals("brewsFotTestCreate"))
                                 select s.Notes;
            // Assert
            //mi che Rate=3
            Assert.AreEqual("notes for brews", notesAfterEdit.FirstOrDefault());
        }

        [TestMethod]
        public void controlla_Brews_DeleteTest()
        {
            // Arrange
            BrewsController controller = new BrewsController();

            //Act
            //id della brews che ha come descrizione "brewsFotTestCreate"
            var idbrewsBeforeDelete = from s in db.Brews
                                            .Where(x => x.Description.Equals("brewsFotTestCreate"))
                                      select s.ID;

            //chiamo il controller per cancellare
            ActionResult result = controller.DeleteConfirmed(idbrewsBeforeDelete.FirstOrDefault());

            //rileggo se esiste l'id dopo la cancellazione
            var idbrewsAfterDelete = from s in db.Brews
                                           .Where(x => x.Description.Equals("brewsFotTestCreate"))
                                     select s.ID;
            // Assert
            //mi aspetto che non ci sia l'id 0
            Assert.AreEqual(0, idbrewsAfterDelete.FirstOrDefault());
        }
    }
}