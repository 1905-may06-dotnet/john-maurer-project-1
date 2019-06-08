using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Addresses : IModels < Elements.Address, Elements.AddressQuery > {

        protected override Elements.Address Read ( Elements.AddressQuery entityArgs ) {

            using ( var context = new Data.PizzaBoxDbContext () ) {

                if ( entityArgs.AddressId != null && entityArgs.AddressId != Guid.Empty )
                    return new Elements.Address ( context.Addresses.Find ( entityArgs.AddressId ) );
                else if (
                        entityArgs.State     != String.Empty &&
                        entityArgs.City      != String.Empty &&
                        entityArgs.Street    != String.Empty &&
                        entityArgs.Zip       != String.Empty &&
                        entityArgs.Apartment != String.Empty 
                ) 
                    return new Elements.Address ( 
                        ( from rec in context.Addresses where 
                            rec.State     != entityArgs.State &&
                            rec.City      != entityArgs.City &&
                            rec.Street    != entityArgs.State &&
                            rec.Zip       != entityArgs.Zip &&
                            rec.Apt       != entityArgs.Apartment 
                          select rec ).FirstOrDefault () );
                else return Elements.Address.Empty;

            }

        }

        protected override HashSet < Elements.Address > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Address > ();

                foreach ( var record in context.Addresses ) 
                    result.Add ( new Elements.Address ( record ) );

                return result;

            }

        }

        public Addresses () : base () {}

        public override Elements.Address Query ( ref Elements.AddressQuery Index ) { return Read ( Index ); }

    }

 }
