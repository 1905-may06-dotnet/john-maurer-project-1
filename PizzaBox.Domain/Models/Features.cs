using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Features : IModels < Elements.Feature, Elements.FeatureQuery > {

        protected override Elements.Feature Read ( Elements.FeatureQuery entityArgs ) {

            if ( entityArgs == Elements.FeatureQuery.Empty ) return Elements.Feature.Empty; else {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                        return new Elements.Feature ( context.Features.Find ( entityArgs.Id ) );
                    else if ( entityArgs.Name != null && entityArgs.Name != String.Empty )
                        return new Elements.Feature 
                            ( ( from rec in context.Features where rec.Name == entityArgs.Name select rec ).FirstOrDefault () );
                    else return Elements.Feature.Empty;

                }

            }

        }

        public Features () : base () { _resource = ReadAll (); }

        public Features ( ref Features addresses ) : base () { _resource = addresses._resource; }

        public Features ( ref ICollection < Data.Entities.Feature > addresses ) {

            foreach ( var index in addresses )
                _resource.Add ( new Models.Elements.Feature ( index ) );

        }

        public override Elements.Feature Query ( ref Elements.FeatureQuery Index ) { return Read ( Index ); }

        public override HashSet < Elements.Feature > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Feature > ();

                foreach ( var record in context.Features ) 
                    result.Add ( new Elements.Feature ( record ) );

                return result;

            }

        }

    }

 }
