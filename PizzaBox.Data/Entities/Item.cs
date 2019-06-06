using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaBox.Data.Entities
{
    [Table("Items", Schema = "PizzaBoxDbSchema")]
    public partial class Item
    {
        public Guid Id { get; set; }
        public Guid? OutletId { get; set; }
        [Required]
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Features { get; set; }

        [ForeignKey("OutletId")]
        [InverseProperty("Items")]
        public virtual Outlet Outlet { get; set; }
    }
}