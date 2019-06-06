using NUnit.Framework;
using System.Linq;

namespace Tests.UnitTest {

    public class Address_UT {

        [ Test ]
        public void Write () {

            var record = new PizzaBox.Domain.Models.Elements.Address ();

            record.Apartment = "A";
            record.City      = "Irvin";
            record.State     = "TX";
            record.Street    = "I should be written only";
            record.Zip       = "91837";

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Addresses.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var records = new PizzaBox.Domain.Models.Addresses ();

            var query   = new PizzaBox.Domain.Models.Elements.AddressQuery {

                Apartment = "A",
                City      = "Kingsville",
                State     = "TX",
                Street    = "I Should be Read Street",
                Zip       = "91837"

            };

            var record  = new PizzaBox.Domain.Models.Elements.Address {

                Apartment = "A",
                City      = "Houston",
                State     = "TX",
                Street    = "I Should be Read Street",
                Zip       = "91837"

            };

            records.Records.Add ( record );

            foreach ( var element in records.Records )
                element.Save ();

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Address.Empty )
                Assert.Pass (); else Assert.Fail ();

        }

        [ Test ]
        public void Delete () {

            var record = new PizzaBox.Domain.Models.Elements.Address ();

            record.Apartment = "A";
            record.City      = "Ft. Worth";
            record.State     = "TX";
            record.Street    = "I Should be Deleted Street";
            record.Zip       = "91837";

            record.Save ();

            System.Guid test = record.Id;

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Addresses.Find ( test ) == null )
                    Assert.Pass (); else Assert.Fail ();

        }

    }

}