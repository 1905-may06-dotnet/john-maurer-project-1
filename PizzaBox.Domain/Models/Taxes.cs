using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Taxes : IModels < Elements.SalesTax, Elements.TaxQuery> {

        protected override Elements.SalesTax Read ( Elements.TaxQuery entityArgs ) {

            if ( entityArgs == Elements.TaxQuery.Empty ) return Elements.SalesTax.Empty; else {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                        return new Elements.SalesTax ( context.StateTaxes.Find ( entityArgs.Id ) );
                    else if ( entityArgs.Territory != null && entityArgs.Territory != String.Empty ) 
                        return new Elements.SalesTax 
                            ( ( from rec in context.StateTaxes where rec.Territory == entityArgs.Territory select rec ).FirstOrDefault () );
                    else return Elements.SalesTax.Empty;

                }

            }

        }

        public Taxes () : base () { _resource = ReadAll (); }

        public Taxes ( ref Taxes addresses ) : base () { _resource = addresses._resource; }

        public Taxes ( ref ICollection < Data.Entities.StateTax > addresses ) {

            foreach ( var index in addresses )
                _resource.Add ( new Models.Elements.SalesTax ( index ) );

        }

        public override HashSet < Elements.SalesTax > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.SalesTax > ();

                foreach ( var record in context.StateTaxes )
                    result.Add ( new Elements.SalesTax ( record ) );

                return result;

            }

        }

        public override Elements.SalesTax Query ( ref Elements.TaxQuery Index ) { return Read ( Index ); }

    }

 }
