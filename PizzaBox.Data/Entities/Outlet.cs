using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaBox.Data.Entities
{
    [Table("Outlets", Schema = "PizzaBoxDbSchema")]
    public partial class Outlet
    {
        public Outlet()
        {
            Addresses = new HashSet<Address>();
            Employees = new HashSet<Employee>();
            Items = new HashSet<Item>();
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        [Required]
        public string Organization { get; set; }

        [InverseProperty("Outlet")]
        public virtual ICollection<Address> Addresses { get; set; }
        [InverseProperty("Outlet")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Outlet")]
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty("Outlet")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}