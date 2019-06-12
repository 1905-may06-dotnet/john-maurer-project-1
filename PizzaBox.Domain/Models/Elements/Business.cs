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

                    var local = context.Outlets.Find ( _resource.Id );

                    if ( context.Outlets.Find ( _resource.Id ) == null ) {

                        context.Attach ( _resource );
                        context.Add < Data.Entities.Outlet > ( _resource );

                    } else {

                        local.Addresses = _resource.Addresses;
                        local.Employees = _resource.Employees;
                        local.Items     = _resource.Items;
                        local.Orders    = _resource.Orders;
                        local.Organization = _resource.Organization;

                        context.Update < Data.Entities.Outlet > ( local );

                    }

                    context.SaveChanges ();

                }

            }

            return new Business ( _resource );

        }

        public override void Delete () {

            lock ( _biz_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    foreach ( var element in context.Items ) if ( element.OutletId == _resource.Id ) {

                        context.Attach ( _resource ).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                        context.Remove ( _resource );

                    }

                    foreach ( var element in context.Orders ) if ( element.OutletId == _resource.Id ) {

                        context.Attach ( element ).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                        context.Remove ( element );

                    }

                    foreach ( var element in context.Employees ) if ( element.OutletId == element.Id ) {

                        var information = element.Person;

                        context.Attach ( element ).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                        context.Remove ( element );
                        context.Attach ( information ).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                        context.Remove ( information );

                    }

                    foreach ( var location in context.Addresses ) if ( location.OutletId != _resource.Id ) {

                        context.Attach ( location );
                        context.Remove ( location );

                    }

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

        public ICollection < Data.Entities.Address > Addresses {

            get { return _resource.Addresses; }
            set { _resource.Addresses = value; }

        }

    }

}
