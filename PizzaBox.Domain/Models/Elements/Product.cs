using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class ProductQuery : EventArgs {

        public static new readonly ProductQuery Empty = new ProductQuery ();

        public Guid   Id       = Guid.Empty;
        public string Name     = String.Empty;
        public string Features = String.Empty;

        public ProductQuery () { }

        public ProductQuery ( Guid Id ) { this.Id = ( Guid ) Id; }

        public ProductQuery ( string Name, string Features ) {

            this.Name     = Name;
            this.Features = Features;

        }

    }

    sealed public class Product : IElement < Data.Entities.Item > {

        private static readonly object _prod_writeLock = new object ();

        public static readonly Product Empty = new Product ();

        public Product () : base () {

            _resource = new Data.Entities.Item ();

            _resource.Id = Guid.NewGuid ();

        }

        public Product ( Data.Entities.Item entity ) { _resource = entity; }

        public Product ( Product product ) { _resource = product._resource; }

        public override Elements.IElement < Data.Entities.Item > Save () {

            if ( _resource.Id == Guid.Empty || _resource.Id == null ) _resource.Id = Guid.NewGuid ();

            lock ( _prod_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( context.Items.Find ( _resource.Id ) == null ) {

                        context.Entry ( _resource );
                        context.Attach < Data.Entities.Item > ( _resource );
                        context.Add    < Data.Entities.Item > ( _resource );

                    }

                    context.SaveChanges ();

                }

            }

            return new Product ( _resource );

        }

        public override void Delete () {

            lock ( _prod_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    context.Remove < Data.Entities.Item > ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public Guid? OutletId { get { return _resource.OutletId; } set { _resource.OutletId = value; } }

        public string Name { get { return _resource.Name; } set { _resource.Name = value; } }

        public double Price { get { return _resource.Cost; } set { _resource.Cost = value; } }

        public string Features { get { return _resource.Features; } set { _resource.Features = value; } }

        public Data.Entities.Outlet Business {

            get { return _resource.Outlet; }
            set { _resource.Outlet = value; }

        }

    }

}
