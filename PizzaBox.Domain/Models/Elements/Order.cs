using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class OrderQuery : EventArgs {

        public static new readonly OrderQuery Empty = new OrderQuery ();

        public Guid      Id          = Guid.Empty;
        public Guid?     OutletId    = Guid.Empty;
        public Guid?     PersonId    = Guid.Empty;
        public DateTime  DateOrdered = new DateTime ();

        public OrderQuery () { }

        public OrderQuery ( Guid Id ) { this.Id = Id; }

        public OrderQuery ( DateTime orderDate ) { DateOrdered = orderDate; }

        public OrderQuery ( Guid? PersonId, Guid? OutletId = null ) {

            if ( PersonId != null ) this.PersonId = PersonId;
            if ( OutletId != null ) this.OutletId = OutletId;

        }

    }

    sealed public class Order : IElement < Data.Entities.Order > {

        private static readonly object _prod_writeLock = new object ();

        public static readonly Order Empty = new Order ();

        public Order () : base () {

            _resource = new Data.Entities.Order ();

            _resource.Id = Guid.NewGuid ();

        }

        public Order ( Data.Entities.Order entity ) { _resource = entity; }

        public Order ( Order order ) { _resource = order._resource; }

        public override Elements.IElement < Data.Entities.Order > Save () {

            if ( _resource.Id == Guid.Empty || _resource.Id == null ) _resource.Id = Guid.NewGuid ();

            lock ( _prod_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( context.Orders.Find ( _resource.Id ) == null ) {

                        context.Entry ( _resource );
                        context.Attach < Data.Entities.Order > ( _resource );
                        context.Add < Data.Entities.Order > ( _resource );
                        
                    }

                    context.SaveChanges ();

                }

            }

            return new Order ( _resource );

        }

        public override void Delete () {

            lock ( _prod_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    context.Remove < Data.Entities.Order > ( _resource );
                    context.SaveChanges();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public Guid? OutletId { get { return _resource.OutletId; } set { _resource.OutletId = value; } }

        public Guid? PersonId { get { return _resource.PersonId; } set { _resource.PersonId = value; } }

        public string Items { get { return _resource.Items; } set { _resource.Items = value; } }

        public DateTime OrderDate { get { return _resource.DateOrdered; } set { _resource.DateOrdered = value; } }

        public double Subtotal { get { return _resource.SubTotal; } set { _resource.SubTotal = value; } }

        public double Total { get { return _resource.Total; } set { _resource.Total = value; } }

        public Data.Entities.Outlet Business {

            get { return _resource.Outlet; }
            set { _resource.Outlet = value; }

        }

        public Data.Entities.Person Customer {

            get { return _resource.Person; }
            set { _resource.Person = value; }

        }

    }

}
