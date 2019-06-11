using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Businesses : IModels < Elements.Business, Elements.BusinessQuery > {

        protected override Elements.Business Read ( Elements.BusinessQuery entityArgs ) {

            using ( var context = new Data.PizzaBoxDbContext () ) {

                if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                    return new Elements.Business ( context.Outlets.Find ( entityArgs.Id ) );
                else if ( entityArgs.Name != null && entityArgs.Name != string.Empty )
                    return new Elements.Business 
                        ( ( from rec in context.Outlets where rec.Organization == entityArgs.Name select rec ).FirstOrDefault () );
                else return Elements.Business.Empty;

            }

        }

        protected override HashSet < Elements.Business > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Business > ();

                foreach ( var record in context.Outlets ) 
                    result.Add ( new Elements.Business ( record ) );

                return result;

            }

        }

        public Businesses () : base () {}

        public override Elements.Business Query ( ref Elements.BusinessQuery Index ) { return Read ( Index ); }

    }

 }
