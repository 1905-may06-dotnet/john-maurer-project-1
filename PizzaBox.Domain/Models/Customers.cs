using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Customers : IModels < Elements.Customer, Elements.CustomerQuery > {

        protected override Elements.Customer Read ( Elements.CustomerQuery entityArgs ) {

            if ( entityArgs == Elements.CustomerQuery.Empty ) return Elements.Customer.Empty; else {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                        return new Elements.Customer ( context.People.Find ( entityArgs.Id ) );
                    else if ( entityArgs.Email != null && entityArgs.Email != String.Empty )
                        return new Elements.Customer 
                            ( ( from rec in context.People where rec.Email == entityArgs.Email select rec ).FirstOrDefault () );
                    else return Elements.Customer.Empty;

                }

            }

        }

        protected override HashSet < Elements.Customer > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Customer > ();

                foreach ( var record in context.People ) 
                    result.Add ( new Elements.Customer ( record ) );

                return result;

            }

        }

        public Customers () : base () { _resource = ReadAll (); }

        public Customers ( ref Customers addresses ) : base () { _resource = addresses._resource; }

        public Customers ( ref ICollection < Data.Entities.Person > addresses ) {

            foreach ( var index in addresses )
                _resource.Add ( new Elements.Customer ( index ) );

        }

        public override Elements.Customer Query ( ref Elements.CustomerQuery Index ) { return Read ( Index ); }

    }

 }
