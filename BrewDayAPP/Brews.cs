//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrewDayAPP
{
    using System;
    using System.Collections.Generic;
    
    public partial class Brews
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public Nullable<int> IdRecipies { get; set; }
        public Nullable<double> BatchSize { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> DateBrew { get; set; }
        public string UserId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Recipies Recipies { get; set; }
    }
}
