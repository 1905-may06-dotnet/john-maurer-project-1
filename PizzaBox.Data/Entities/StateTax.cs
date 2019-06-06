using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaBox.Data.Entities
{
    [Table("StateTaxes", Schema = "PizzaBoxDbSchema")]
    public partial class StateTax
    {
        public Guid Id { get; set; }
        [Required]
        public string Territory { get; set; }
        public double TaxRate { get; set; }
    }
}