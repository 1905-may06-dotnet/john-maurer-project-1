using NUnit.Framework;

namespace Tests.UnitTest {

    public class Order_UT {

        [ Test ]
        public void Write () {

            var record = new PizzaBox.Domain.Models.Elements.Order ();

            record.Items     = "none";
            record.Subtotal  = 912.45;
            record.Total     = 1203.01;
            record.OrderDate = System.DateTime.Now;

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Orders.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var records = new PizzaBox.Domain.Models.Orders ();
            var record  = new PizzaBox.Domain.Models.Elements.Order {

                    Items     = "none",
                    Subtotal  = 90.04,
                    Total     = 101.10,
                    OrderDate = System.DateTime.Now

            };

            var query   = new PizzaBox.Domain.Models.Elements.OrderQuery { DateOrdered = record.OrderDate };

            records.Records.Add ( record );

            foreach ( var element in records.Records )
                element.Save ();

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Order.Empty )
                Assert.Pass (); else Assert.Fail ();

        }

        [ Test ]
        public void Delete () {

            var record = new PizzaBox.Domain.Models.Elements.Order ();

            record.Items     = "none";
            record.Subtotal  = 912.45;
            record.Total     = 1203.01;
            record.OrderDate = System.DateTime.Now;

            record.Save ();

            System.Guid test = record.Id;

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Orders.Find ( test ) == null )
                    Assert.Pass (); else Assert.Fail ();

        }

    }

}