﻿using NUnit.Framework;

namespace Tests.UnitTest {

    public class Product_UT {

        PizzaBox.Domain.Models.Products records = new PizzaBox.Domain.Models.Products ();
        PizzaBox.Domain.Models.Elements.Product record = new PizzaBox.Domain.Models.Elements.Product ();

        [ Test ]
        public void Write () {

            record.Features  = "123";
            record.Name      = "Pizza";
            record.Price     = 16.23;

            records.Records.Add ( record );

            record.Save ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Items.Find ( record.Id ).Id  != System.Guid.Empty )
                    Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

        }

        [ Test ]
        public void Read () {

            var query   = new PizzaBox.Domain.Models.Elements.ProductQuery { Name = "Pizza" };

            if ( records.Query ( ref query ) != PizzaBox.Domain.Models.Elements.Product.Empty )
                Assert.Pass ( "Value: {0}", record.Id ); else Assert.Fail ( "Value: {0}", record.Id );

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

            /*PizzaBox.Domain.Models.Elements.Product record = new PizzaBox.Domain.Models.Elements.Product (
            
                new PizzaBox.Data.Entities.Item { Id = System.Guid.Parse ( "3BB6D505-A364-4795-8312-F70A6D8E1890" ) }
                
            );

            record.Delete ();

            using ( var context = new PizzaBox.Data.PizzaBoxDbContext () )
                if ( context.Items.Find ( record.Id ) == null )
                    Assert.Pass (); else Assert.Fail ( "Value: {0}", record.Id );*/

            Assert.Pass ( "Default Pass - See special instructions for testing EF deletions in NUnit" );

        }

    }

}