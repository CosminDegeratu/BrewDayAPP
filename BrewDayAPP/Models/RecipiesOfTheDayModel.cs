using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewDayAPP
{
    public class RecipiesOfTheDay
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public Nullable<double> batchSize { get; set; }
        public int Rate { get; set; }
    }
}