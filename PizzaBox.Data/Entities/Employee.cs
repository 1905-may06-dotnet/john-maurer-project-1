using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaBox.Data.Entities
{
    [Table("Employees", Schema = "PizzaBoxDbSchema")]
    public partial class Employee
    {
        public Guid Id { get; set; }
        public Guid? OutletId { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string WageType { get; set; }
        public double Wage { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Status { get; set; }
        [Column("Person_Id")]
        public Guid PersonId { get; set; }

        [ForeignKey("OutletId")]
        [InverseProperty("Employees")]
        public virtual Outlet Outlet { get; set; }
        [ForeignKey("PersonId")]
        [InverseProperty("Employees")]
        public virtual Person Person { get; set; }
    }
}