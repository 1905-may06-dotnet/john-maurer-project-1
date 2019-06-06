using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaBox.Data.Entities
{
    [Table("Orders", Schema = "PizzaBoxDbSchema")]
    public partial class Order
    {
        public Guid Id { get; set; }
        public Guid? OutletId { get; set; }
        public Guid? PersonId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateOrdered { get; set; }
        [Required]
        public string Items { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }

        [ForeignKey("OutletId")]
        [InverseProperty("Orders")]
        public virtual Outlet Outlet { get; set; }
        [ForeignKey("PersonId")]
        [InverseProperty("Orders")]
        public virtual Person Person { get; set; }
    }
}