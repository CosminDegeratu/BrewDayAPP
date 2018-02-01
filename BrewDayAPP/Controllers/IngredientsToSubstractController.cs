using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrewDayAPP.Controllers
{
    public class IngredientsToSubstractController : Controller
    {
        // GET: IngredientToSubstract
        public ActionResult _Index(int? recipiesID, int batchSize)
        {
            BrewDayDBEntities db = new BrewDayDBEntities();
            string query = "select r.ID as IdRecipie, i.ID as IdIngredient, i.[Description], i.UnitMeasure, i.Quantity, ir.AbsolutQuantity, " + batchSize + " as [Batchsize], (ir.AbsolutQuantity * " + batchSize + ") as QuantityToSubstract, (i.Quantity - (ir.AbsolutQuantity * " + batchSize + ")) as QuantityAfterBrew, i.Threshold" +
                                                                        " from Ingredients as i inner join" +
                                                                        " IngredientRecipe ir on i.ID = ir.IdIngredients inner join" +
                                                                        " Recipies as r on ir.IdRecipes = r.ID " +
                                                                        " where r.ID =" + recipiesID +
                                                                        " group by r.ID, i.ID, i.[Description], i.UnitMeasure, i.Quantity, ir.AbsolutQuantity, i.Threshold";
            IEnumerable<IngredientToSubstract> data = db.Database.SqlQuery<IngredientToSubstract>(query);
            return PartialView(data.ToList());
        }

    }
}