//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PremiumJustMuesliApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MuesliIngredient
    {
        public int Id { get; set; }
        public int MuesliId { get; set; }
        public int IngredientId { get; set; }
    
        public virtual Ingredient Ingredient { get; set; }
        public virtual Muesli Muesli { get; set; }
    }
}
