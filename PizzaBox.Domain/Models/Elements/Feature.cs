using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class FeatureQuery : EventArgs {

        public static new readonly FeatureQuery Empty = new FeatureQuery ();

        public Guid?   Id   = Guid.Empty;
        public string  Name = String.Empty;

        public FeatureQuery () { }

        public FeatureQuery ( string Name ) { this.Name = Name; }

        public FeatureQuery ( FeatureQuery featureArgs ) { this.Name = featureArgs.Name; }

        public FeatureQuery ( Guid Id ) { if ( Id != null && Id != Guid.Empty ) this.Id = Id; }

    }

    sealed public class Feature : IElement < Data.Entities.Feature > {

        private static readonly object _feat_writeLock = new object ();

        public static readonly Feature Empty = new Feature();

        public Feature () : base () { _resource = new Data.Entities.Feature (); }

        public Feature ( Data.Entities.Feature entity ) { _resource = entity; }

        public Feature ( Feature feature ) { _resource = feature._resource; }

        public override Elements.IElement < Data.Entities.Feature > Save () {

            if ( _resource.Id == Guid.Empty || _resource.Id == null ) _resource.Id = Guid.NewGuid ();

            lock ( _feat_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( context.Features.Find ( _resource.Id ) == null ) {

                        context.Entry ( _resource );
                        context.Attach < Data.Entities.Feature > ( _resource );
                        context.Add < Data.Entities.Feature > ( _resource );

                    }

                    context.SaveChanges ();

                }

            }

            return new Feature ( _resource );

        }

        public override void Delete () {

            lock ( _feat_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    context.Remove < Data.Entities.Feature > ( _resource );
                    context.SaveChanges();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public Guid? OutletId { get { return _resource.OutletId; } set { _resource.OutletId = value; } }

        public string Name { get { return _resource.Name; } set { _resource.Name = value; } }

        public double Price { get { return _resource.Cost; } set { _resource.Cost = value; } }

        public Data.Entities.Outlet Business {

            get { return _resource.Outlet; }
            set { _resource.Outlet = value; }

        }

    }

}
