using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Products : IModels < Elements.Product, Elements.ProductQuery > {

        protected override Elements.Product Read ( Elements.ProductQuery entityArgs ) {

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

        protected override HashSet < Elements.Product > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Product > ();

                foreach ( var record in context.Items ) 
                    result.Add ( new Elements.Product ( record ) );

                return result;

            }

        }

        public Products () : base () {}

        public override Elements.Product Query ( ref Elements.ProductQuery Index ) { return Read ( Index ); }

    }

 }
