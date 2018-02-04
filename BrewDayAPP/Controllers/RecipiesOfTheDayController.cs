using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrewDayAPP.Controllers
{
    public class RecipiesOfTheDayController : Controller
    {
        // GET: RecipiesOfTheDay
        public ActionResult _Index()
        {
            BrewDayDBEntities db = new BrewDayDBEntities();
            //recupera dal model parametri che mi servono per la spUpdateQuantityIngredients
            var userID = User.Identity.GetUserId();
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            if (User.Identity.GetUserName().Equals(ConfigurationManager.AppSettings["SuperUser"]))
            {
                //lancia la spRecuperaRecipiesOfTheDayAll
                var command = new SqlCommand("spRecuperaRecipiesOfTheDayAll", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                //lancia la spUpdateQuantityIngredients con parametro userID
                var command = new SqlCommand("spRecuperaRecipiesOfTheDay", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userID);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            var query = "select ID,[Description], [batchSize],Rate from RecipiesofTheDay";
            IEnumerable<RecipiesOfTheDay> data = db.Database.SqlQuery<RecipiesOfTheDay>(query);
            return PartialView(data.ToList());
        }
    }
}