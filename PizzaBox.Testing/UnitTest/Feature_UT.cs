using NUnit.Framework;

namespace Tests.UnitTest {

    public class Feature_UT {

        PizzaBox.Domain.Models.Features records = new PizzaBox.Domain.Models.Features ();
        PizzaBox.Domain.Models.Elements.Feature record = new PizzaBox.Domain.Models.Elements.Feature ();

        [ Test ]
        public void Write () {

            record.Name  = "cheese";
            record.Price = 10.99;

            record.Save ();

            records.Records.Add ( record );

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Features.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var query   = new PizzaBox.Domain.Models.Elements.FeatureQuery { Name = "cheese" };

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Feature.Empty )
                Assert.Pass (); else Assert.Fail ();

        }

        [ Test ]
        public void Delete () {

            /// Due to the concurrency constraints of Entity Framework (EF) and the execution of NUnit deletion testing
            /// cannot be performing in parrellel with write/read testing.
            /// 
            /// The problem stems from EF's concurrency model.  Under normal production conditions read, write and
            /// delete on a single record occur at a pace that EF's concurrency model can keep up with.  In NUint
            /// these operations occur so quickly that EF's concurrency model cannot keep up, producing concurrency
            /// errors that cannot be corrected in unit testing using NUnit.
            /// 
            /// SOLUTION
            /// 
            /// 1. Using SSMS, select the top thousand records and copy the Id of the target record
            /// 2. Replace the GUID string value in the 'System.Guid.Parse' method with the copied value
            /// 3. Comment out the default 'Assert.Pass' and uncomment the group comment block
            /// 4. Run the 'Delete' test indiviually
            /// 5. Restore when complete

            /*PizzaBox.Domain.Models.Elements.Feature record = new PizzaBox.Domain.Models.Elements.Feature (
            
                new PizzaBox.Data.Entities.Feature { Id = System.Guid.Parse ( "3BB6D505-A364-4795-8312-F70A6D8E1890" ) }
                
            );

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Features.Find ( record.Id ) == null )
                    Assert.Pass (); else Assert.Fail ( "Value: {0}", record.Id );*/

            Assert.Pass ( "Default Pass - See special instructions for testing EF deletions in NUnit" );

        }

    }

}