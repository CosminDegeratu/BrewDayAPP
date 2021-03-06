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
    
    public partial class Ingredients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingredients()
        {
            this.IngredientRecipe = new HashSet<IngredientRecipe>();
            this.ShoppingList = new HashSet<ShoppingList>();
        }
    
        public int ID { get; set; }
        public string Description { get; set; }
        public Nullable<double> Quantity { get; set; }
        public string UnitMeasure { get; set; }
        public Nullable<double> Threshold { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredientRecipe> IngredientRecipe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingList> ShoppingList { get; set; }
    }
}
