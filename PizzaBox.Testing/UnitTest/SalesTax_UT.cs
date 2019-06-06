using NUnit.Framework;

namespace Tests.UnitTest {

    public class SalesTax_UT {

        [ Test ]
        public void Write () {

            var record = new PizzaBox.Domain.Models.Elements.SalesTax ();

            record.Territory = "Mars";
            record.Rate      = 3.24;

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.StateTaxes.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var records = new PizzaBox.Domain.Models.Taxes ();
            var query   = new PizzaBox.Domain.Models.Elements.TaxQuery { Territory = "JR" };
            var record  = new PizzaBox.Domain.Models.Elements.SalesTax {

                    Territory = "JR",
                    Rate      = 3.09

            };

            records.Records.Add ( record );

            foreach ( var element in records.Records ) element.Save ();

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.SalesTax.Empty )
                Assert.Pass (); else Assert.Fail ();


        }

        [ Test ]
        public void Delete () {

            var record = new PizzaBox.Domain.Models.Elements.SalesTax ();

            record.Territory = "Mars";
            record.Rate      = 3.24;

            record.Save ();

            System.Guid test = record.Id;

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.StateTaxes.Find ( test ) == null )
                    Assert.Pass (); else Assert.Fail ();

        }

    }

}