using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class BusinessQuery : EventArgs {

        public static new readonly BusinessQuery Empty = new BusinessQuery ();

        public Guid   Id   = Guid.Empty;
        public string Name = string.Empty;

        public BusinessQuery () { }

        public BusinessQuery ( Guid Id ) { this.Id = Id; }

        BusinessQuery ( string BusinessName ) { Name = BusinessName; }

    }

    sealed public class Business : IElement < Data.Entities.Outlet > {

        private static readonly object _biz_writeLock = new object ();

        public static readonly Business Empty = new Business ();

        public Business () : base () {

            _resource = new Data.Entities.Outlet ();

            _resource.Id = Guid.NewGuid ();

        }

        public Business ( Data.Entities.Outlet entity ) { _resource = entity; }

        public Business ( Business business ) { _resource = business._resource; }

        public override Elements.IElement < Data.Entities.Outlet > Save () {

            lock ( _biz_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( context.Outlets.Find ( _resource.Id ) == null ) {

                        context.Attach ( _resource );
                        context.Add < Data.Entities.Outlet > ( _resource );

                    } else context.Update < Data.Entities.Outlet > ( _resource );

                    context.SaveChanges ();

                }

            }

            return new Business ( _resource );

        }

        public override void Delete () {

            lock ( _biz_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {
                    
                    context.Remove < Data.Entities.Outlet > ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public string Name { get { return _resource.Organization; } set { _resource.Organization = value; } }

        public ICollection < Data.Entities.Order > Orders { get { return _resource.Orders; } set { _resource.Orders = value; } }

        public ICollection < Data.Entities.Employee > Employees {

            get { return _resource.Employees; }
            set { _resource.Employees = value; }

        }

        public ICollection < Data.Entities.Item > Items {

            get { return _resource.Items; }
            set { _resource.Items = value; }

        }

        public ICollection < Data.Entities.Feature > Features {

            get { return _resource.Features; }
            set { _resource.Features = value; }

        }

        public ICollection < Data.Entities.Address > Addresses {

            get { return _resource.Addresses; }
            set { _resource.Addresses = value; }

        }

    }

}
