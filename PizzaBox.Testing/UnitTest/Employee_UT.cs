using NUnit.Framework;
using System.Linq;

namespace Tests.UnitTest {

    public class Employee_UT {

        [ Test ]
        public void Write () {

            var record = new PizzaBox.Domain.Models.Elements.Employee ();

            record.Username   = "working@dat.com";
            record.Password   = "oyea";
            record.Position   = "cook";
            record.Status     = "active";
            record.Wage       = 16.50;
            record.WageType   = "hourly";

            record.Information = new PizzaBox.Data.Entities.Person {

                DoB   = System.DateTime.UtcNow,
                Email = "employeeone@employed.com",
                Fname = "me",
                Mname = "so",
                Lname = "soTired",
                Phone = "323-555-0908",

            };

            new PizzaBox.Domain.Models.Elements.Customer ( record.Information ).Save ();

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Employees.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var records = new PizzaBox.Domain.Models.Employees ();

            var query   = new PizzaBox.Domain.Models.Elements.EmployeeQuery { Username  = "working@fordat.com" };

            var record  = new PizzaBox.Domain.Models.Elements.Employee {

                Username   = "working@fordat.com",
                Password   = "oyea",
                Position   = "cook",
                Status     = "active",
                Wage       = 16.50,
                WageType   = "hourly",

                Information = new PizzaBox.Data.Entities.Person {

                    DoB   = System.DateTime.UtcNow,
                    Email = "employeefour@employed.com",
                    Fname = "me",
                    Mname = "so",
                    Lname = "soTired",
                    Phone = "333-455-0918",

                }

            };

            new PizzaBox.Domain.Models.Elements.Customer ( record.Information ).Save ();

            records.Records.Add ( record );

            foreach ( var element in records.Records )
                element.Save ();

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Employee.Empty )
                Assert.Pass (); else Assert.Fail ();

        }

        [ Test ]
        public void Delete () {

            var record = new PizzaBox.Domain.Models.Elements.Employee ();

            record.Username   = "hard@money.com";
            record.Password   = "oyea";
            record.Position   = "cook";
            record.Status     = "active";
            record.Wage       = 16.50;
            record.WageType   = "hourly";

            record.Information = new PizzaBox.Data.Entities.Person {

                DoB   = System.DateTime.UtcNow,
                Email = "employeedel@employed.com",
                Fname = "me",
                Mname = "so",
                Lname = "soTired",
                Phone = "333-555-0908",

            };

            new PizzaBox.Domain.Models.Elements.Customer ( record.Information ).Save ();

            record.Save ();

            System.Guid test = record.Id;

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Employees.Find ( test ) == null )
                    Assert.Pass (); else Assert.Fail ();

        }

    }

}