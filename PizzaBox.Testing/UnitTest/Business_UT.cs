using NUnit.Framework;
using System.Linq;

namespace Tests.UnitTest {

    public class Business_UT {

        [ Test ]
        public void Write () {

            var record = new PizzaBox.Domain.Models.Elements.Business ();

            record.Name = "Write Test Pizzoria";

            record.Addresses.Add ( new PizzaBox.Data.Entities.Address {

                Apt    = "A",
                City   = "Ontario",
                State  = "TX",
                Street = "Write Employee Street",
                Zip    = "91837"

            } );

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Outlets.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Addresses.ToString () ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var records = new PizzaBox.Domain.Models.Businesses ();
            var query   = new PizzaBox.Domain.Models.Elements.BusinessQuery { Name = "Read Test Pizzoria" };
            var record  = new PizzaBox.Domain.Models.Elements.Business {

                    Name  = "Read Test Pizzoria"

            };

            record.Addresses.Add ( 
                
                new PizzaBox.Data.Entities.Address {

                    Apt    = "A",
                    City   = "Vegas",
                    State  = "TX",
                    Street = "Write Employee Street",
                    Zip    = "91837"

                }

            );

            records.Records.Add ( record );

            foreach ( var element in records.Records )
                element.Save ();

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Business.Empty )
                Assert.Pass (); else Assert.Fail ();

        }

        [ Test ]
        public void Delete () {

            var record = new PizzaBox.Domain.Models.Elements.Business ();

            record.Name = "Delete Test Pizzoria";

            record.Addresses.Add ( ( new PizzaBox.Data.Entities.Address {

                Apt    = "A",
                City   = "Orlando",
                State  = "TX",
                Street = "Write Employee Street",
                Zip    = "91837"

            } ) );

            record.Save ();
            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Outlets.Find ( record.Id ) == null )
                    Assert.Pass (); else Assert.Fail ();

        }

    }

}
