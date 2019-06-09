using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaBox.Data.Entities
{
    [Table("Addresses", Schema = "PizzaBoxDbSchema")]
    public partial class Address
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? OutletId { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Zip { get; set; }
        public string Apt { get; set; }
        [Column("Order_Id")]
        public Guid? OrderId { get; set; }
        [Column("Person_Id")]
        public Guid? PersonId1 { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("Addresses")]
        public virtual Order Order { get; set; }
        [ForeignKey("OutletId")]
        [InverseProperty("Addresses")]
        public virtual Outlet Outlet { get; set; }
        [ForeignKey("PersonId1")]
        [InverseProperty("Addresses")]
        public virtual Person PersonId1Navigation { get; set; }
    }
}