using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class TaxQuery : EventArgs {

        public static new readonly TaxQuery Empty = new TaxQuery ();

        public Guid   Id        = Guid.Empty;
        public string Territory = String.Empty; 

        public TaxQuery () { }

        public TaxQuery ( Guid Id ) { this.Id = Id; }

        public TaxQuery ( string Territory ) { this.Territory = Territory; }

    }

    sealed public class SalesTax : IElement < Data.Entities.StateTax > {

        private static readonly object _tax_writeLock = new object ();

        public static readonly SalesTax Empty = new SalesTax ();

        public SalesTax () : base () {

            _resource = new Data.Entities.StateTax ();

            _resource.Id = Guid.NewGuid ();

        }

        public SalesTax ( Data.Entities.StateTax entity ) { _resource = entity; }

        public SalesTax ( SalesTax salesTax ) { _resource = salesTax._resource; }

        public override Elements.IElement < Data.Entities.StateTax > Save () {

            lock ( _tax_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {    

                    if ( context.StateTaxes.Find ( _resource.Id ) == null ) {

                        context.Entry ( _resource );
                        context.Attach ( _resource );
                        context.Add < Data.Entities.StateTax > ( _resource );

                    } else context.Update < Data.Entities.StateTax > ( _resource );

                    context.SaveChanges ();

                }

            }

            return new SalesTax ( _resource );

        }

        public override void Delete () {

            lock ( _tax_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    context.Remove < Data.Entities.StateTax > ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public string Territory { get { return _resource.Territory; } set { _resource.Territory = value; } }

        public double Rate { get { return _resource.TaxRate; } set { _resource.TaxRate = value; } }

    }

}
