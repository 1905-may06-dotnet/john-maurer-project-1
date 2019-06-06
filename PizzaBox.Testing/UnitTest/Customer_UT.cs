using NUnit.Framework;

namespace Tests.UnitTest {

    public class Customer_UT {

        [ Test ]
        public void Write () {

            var record = new PizzaBox.Domain.Models.Elements.Customer ();

            record.DoB        = System.DateTime.UtcNow;
            record.Email      = "mesohungry@famished.com";
            record.FirstName  = "me";
            record.MiddleName = "so";
            record.LastName   = "soTired";
            record.Phone      = "333-555-0908";

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.People.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var records = new PizzaBox.Domain.Models.Customers ();

            var query   = new PizzaBox.Domain.Models.Elements.CustomerQuery {

                Email = "mesohungry@famished.com"

            };

            var record  = new PizzaBox.Domain.Models.Elements.Customer {

                DoB        = System.DateTime.UtcNow,
                Email      = "mesohungry@famished.com",
                FirstName  = "me",
                MiddleName = "so",
                LastName   = "soTired",
                Phone      = "333-555-0908"

            };

            records.Records.Add ( record );

            foreach ( var element in records.Records )
                element.Save ();

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Customer.Empty )
                Assert.Pass (); else Assert.Fail ();

        }

        [ Test ]
        public void Delete () {

            var record = new PizzaBox.Domain.Models.Elements.Customer ();

            record.DoB        = System.DateTime.UtcNow;
            record.Email      = "smegul@givesusachance.com";
            record.FirstName  = "smegul";
            record.MiddleName = "precious";
            record.LastName   = "evil";
            record.Phone      = "433-081-4761";

            record.Save ();

            System.Guid test = record.Id;

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.People.Find ( test ) == null )
                    Assert.Pass (); else Assert.Fail ();

        }

    }

}
