using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Businesses : IModels < Elements.Business, Elements.BusinessQuery > {

        protected override Elements.Business Read ( Elements.BusinessQuery entityArgs ) {

            if ( entityArgs == Elements.BusinessQuery.Empty ) return Elements.Business.Empty; else {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                        return new Elements.Business ( context.Outlets.Find ( entityArgs.Id ) );
                    else if ( entityArgs.Name != null && entityArgs.Name != string.Empty )
                        return new Elements.Business 
                            ( ( from rec in context.Outlets where rec.Organization == entityArgs.Name select rec ).FirstOrDefault () );
                    else return Elements.Business.Empty;

                }

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

        public Businesses () : base () { _resource = ReadAll (); }

        public Businesses ( ref Businesses addresses ) : base () { _resource = addresses._resource; }

        public Businesses ( ref ICollection < Data.Entities.Outlet > addresses ) {

            foreach ( var index in addresses )
                _resource.Add ( new Models.Elements.Business ( index ) );

        }

        public override Elements.Business Query ( ref Elements.BusinessQuery Index ) { return Read ( Index ); }

    }

 }
