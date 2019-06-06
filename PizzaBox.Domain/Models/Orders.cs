using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Orders : IModels < Elements.Order, Elements.OrderQuery > {

        protected override Elements.Order Read ( Elements.OrderQuery entityArgs ) {

            if ( entityArgs == Elements.OrderQuery.Empty ) return Elements.Order.Empty; else {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                        return new Elements.Order ( context.Orders.Find ( entityArgs.Id ) );
                    else if ( entityArgs.OutletId != null && entityArgs.OutletId != Guid.Empty )
                        return new Elements.Order 
                            ( ( from rec in context.Orders where rec.OutletId == entityArgs.OutletId select rec ).FirstOrDefault () );
                    else if ( entityArgs.PersonId != null && entityArgs.PersonId != Guid.Empty )
                        return new Elements.Order 
                            ( ( from rec in context.Orders where rec.PersonId == entityArgs.PersonId select rec ).FirstOrDefault () );
                    else if ( entityArgs.DateOrdered != null )
                        return new Elements.Order 
                            ( ( from rec in context.Orders where rec.DateOrdered == entityArgs.DateOrdered select rec ).FirstOrDefault () );
                    else return Elements.Order.Empty;

                }

            }

        }

        protected override HashSet < Elements.Order > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Order > ();

                foreach ( var record in context.Orders ) 
                    result.Add ( new Elements.Order ( record ) );

                return result;

            }

        }

        public Orders () : base () { _resource = ReadAll (); }

        public Orders ( ref Orders addresses ) : base () { _resource = addresses._resource; }

        public Orders ( ref ICollection < Data.Entities.Order > addresses ) {

            foreach ( var index in addresses )
                _resource.Add ( new Models.Elements.Order ( index ) );

        }

        public override Elements.Order Query ( ref Elements.OrderQuery Index ) { return Read ( Index ); }

    }

 }
