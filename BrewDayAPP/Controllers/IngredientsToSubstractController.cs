using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrewDayAPP.Controllers
{
    public class IngredientsToSubstractController : Controller
    {
        // GET: IngredietToSubstract
        public ActionResult _Index(int? recipiesID,double? batchSize)
        {
            BrewDayDBEntities db = new BrewDayDBEntities();

            //const string strApplicationCode = "APP";
            //int userid = Convert.ToInt32(HttpContext.Current.Session["sysUserId"]);
            //var applist = GetAvailableItems(userid, strApplicationCode);

            string query = "select r.ID as IdRecipie, i.ID as IdIngredient, i.[Description], i.UnitMeasure, i.Quantity, ir.AbsolutQuantity, b.[Batchsize], (ir.AbsolutQuantity * b.[BatchSize]) as QuantityToSubstract, (i.Quantity - (ir.AbsolutQuantity * b.[BatchSize])) as QuantityAfterBrew, i.Threshold" +
                                                                        " from Ingredients as i inner join" +
                                                                        " IngredientRecipe ir on i.ID = ir.IdIngredients inner join" +
                                                                        " Recipies as r on ir.IdRecipes = r.ID inner join" +
                                                                        " Brews as b on r.ID = b.IdRecipies" +
                                                                        " where r.ID =" + recipiesID +" and b.[BatchSize]=" + batchSize +
                                                                        " group by r.ID, i.ID, i.[Description], i.UnitMeasure, i.Quantity, ir.AbsolutQuantity, b.[Batchsize], i.Threshold";
            IEnumerable<IngredientToSubstract> data = db.Database.SqlQuery<IngredientToSubstract>(query);


            //BrewDayDBEntities db = new BrewDayDBEntities();
            //IList<IngredientsToSubstract> = db.Database.ExecuteSqlCommand("select r.ID as IdRecipie, i.ID as IdIngredient, i.[Description], i.UnitMeasure, i.Quantity, ir.AbsolutQuantity, b.[Batchsize], (ir.AbsolutQuantity* b.[BatchSize]) as QuantityToSubstract, (i.Quantity - (ir.AbsolutQuantity* b.[BatchSize])) as QuantityAfterBrew, i.Threshold" +
            //                                                        " from Ingredients as i inner join" +
            //                                                        " IngredientRecipe ir on i.ID = ir.IdIngredients inner join" +
            //                                                        " Recipies as r on ir.IdRecipes = r.ID inner join" +
            //                                                        " Brews as b on r.ID = b.IdRecipies" +
            //                                                        " where r.ID =" + recipiesID + 
            //                                                        " group by r.ID, i.ID, i.[Description], i.UnitMeasure, i.Quantity, ir.AbsolutQuantity, b.[Batchsize], i.Threshold");
            return PartialView(data.ToList());
        }

    }
}