using NUnit.Framework;

namespace Tests.UnitTest {

    public class Feature_UT {

        [ Test ]
        public void Write () {

            var record = new PizzaBox.Domain.Models.Elements.Feature ();

            record.Name  = "cheese";
            record.Price = 10.99;

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Features.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var records = new PizzaBox.Domain.Models.Features ();
            var query   = new PizzaBox.Domain.Models.Elements.FeatureQuery { Name = "more cheese" };
            var record  = new PizzaBox.Domain.Models.Elements.Feature {

                    Name  = "more cheese",
                    Price = 11.99

            };

            records.Records.Add ( record );

            foreach ( var element in records.Records )
                element.Save ();

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Feature.Empty )
                Assert.Pass (); else Assert.Fail ();

        }

        [ Test ]
        public void Delete () {

            var record = new PizzaBox.Domain.Models.Elements.Feature ();

            record.Name  = "even more cheese";
            record.Price = 12.99;

            record.Save ();

            System.Guid test = record.Id;

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Features.Find ( test ) == null )
                    Assert.Pass (); else Assert.Fail ();

        }

    }

}