using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BrewDayAPP
{
    public class IngredientToSubstract
    {

        public int IdRecipie { get; set; }
        public int IdIngredient { get; set; }
        public string Description { get; set; }
        public string UnitMeasure { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> AbsolutQuantity { get; set; }
        public Nullable<double> BatchSize { get; set; }
        public Nullable<double> QuantityToSubstract { get; set; }
        public Nullable<double> QuantityAfterBrew { get; set; }
        public Nullable<double> Threshold { get; set; }
    }
}