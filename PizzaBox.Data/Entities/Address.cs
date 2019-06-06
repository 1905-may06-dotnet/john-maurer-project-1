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

        [ForeignKey("OutletId")]
        [InverseProperty("Addresses")]
        public virtual Outlet Outlet { get; set; }
        [ForeignKey("PersonId")]
        [InverseProperty("Addresses")]
        public virtual Person Person { get; set; }
    }
}