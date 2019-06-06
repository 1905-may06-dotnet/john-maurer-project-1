using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaBox.Data.Entities
{
    [Table("Features", Schema = "PizzaBoxDbSchema")]
    public partial class Feature
    {
        public Guid Id { get; set; }
        public Guid? OutletId { get; set; }
        [Required]
        public string Name { get; set; }
        public double Cost { get; set; }

        [ForeignKey("OutletId")]
        [InverseProperty("Features")]
        public virtual Outlet Outlet { get; set; }
    }
}