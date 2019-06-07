using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Products : IModels < Elements.Product, Elements.ProductQuery > {

        protected override Elements.Product Read ( Elements.ProductQuery entityArgs ) {

            if ( entityArgs == Elements.ProductQuery.Empty ) return Elements.Product.Empty; else {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                        return new Elements.Product ( context.Items.Find ( entityArgs.Id ) );
                    else if ( entityArgs.Name != null && entityArgs.Name != String.Empty ) 
                        return new Elements.Product ( 
                            ( from rec in context.Items where rec.Name == entityArgs.Name select rec ).FirstOrDefault () );    
                    else if ( entityArgs.Features != null && entityArgs.Features != String.Empty )
                        return new Elements.Product ( 
                            ( from rec in context.Items where rec.Features == entityArgs.Features select rec ).FirstOrDefault () );
                    else return Elements.Product.Empty;

                }

            }

        }

        public Products () : base () { _resource = ReadAll (); }

        public Products ( ref Products addresses ) : base () { _resource = addresses._resource; }

        public Products ( ref ICollection < Data.Entities.Item > addresses ) {

            foreach ( var index in addresses )
                _resource.Add ( new Models.Elements.Product ( index ) );

        }

        public override Elements.Product Query ( ref Elements.ProductQuery Index ) { return Read ( Index ); }

        public override HashSet < Elements.Product > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Product > ();

                foreach ( var record in context.Items ) 
                    result.Add ( new Elements.Product ( record ) );

                return result;

            }

        }

    }

 }
