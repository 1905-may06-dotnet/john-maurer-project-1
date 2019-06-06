using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Data.Entities;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class AddressQuery : EventArgs {

        new public static AddressQuery Empty = new AddressQuery ();

        public Guid? AddressId = Guid.Empty;

        public string State     = String.Empty;
        public string City      = String.Empty;
        public string Street    = String.Empty;
        public string Zip       = String.Empty;
        public string Apartment = String.Empty;

        public AddressQuery () {}

        public AddressQuery ( Guid? AddressId ) { this.AddressId = AddressId; }

        public AddressQuery ( string State, string City, string Street, string Apartment, string Zip ) {

            this.State     = State;
            this.City      = City;
            this.Street    = Street;
            this.Zip       = Zip;
            this.Apartment = Apartment;

        }

        public AddressQuery ( ref AddressQuery addressArgs ) {

            AddressId = addressArgs.AddressId;
            State     = addressArgs.State;
            City      = addressArgs.City;
            Street    = addressArgs.State;
            Zip       = addressArgs.Zip;
            Apartment = addressArgs.Apartment;

        }

    }

    sealed public class Address : IElement < Data.Entities.Address > { 

        private static readonly object _addr_writeLock = new object ();

        public static readonly Address Empty = new Address ();

        public Address () : base () { _resource = new Data.Entities.Address (); }

        public Address ( Data.Entities.Address entity ) { _resource = entity; }

        public Address ( Address address ) { _resource = address._resource; }

        public override Elements.IElement < Data.Entities.Address > Save () {

            if ( _resource.Id == Guid.Empty || _resource.Id == null ) _resource.Id = Guid.NewGuid ();

            lock ( _addr_writeLock ) { 

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( context.Addresses.Find ( _resource.Id ) == null ) {

                        context.Entry  ( _resource );
                        context.Attach ( _resource );
                        context.Add < Data.Entities.Address > ( _resource );

                    }
                    
                    context.SaveChanges ();

                }

            }

            return new Address ( _resource );

        }

        public override void Delete () {

            lock ( _addr_writeLock ) { 

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    context.Remove < Data.Entities.Address > ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public Guid? OutletId { get { return OutletId; } set { _resource.OutletId = value; } }

        public Guid? PersonId { get { return PersonId; } set { _resource.OutletId = value; } }

        public Guid? ResidentId {

            get { return _resource.PersonId != Guid.Empty ?_resource.PersonId : _resource.OutletId; }

            set {

                if ( _resource.PersonId == Guid.Empty && _resource.OutletId != Guid.Empty )
                    throw new ArgumentException ( "Address has no recorded resident!\n Person and Outlet Id's are null" );

                if ( _resource.PersonId != Guid.Empty ) _resource.PersonId = value;
                else _resource.OutletId = value;

            }

        }

        public bool IsOutlet { get { return _resource.PersonId == Guid.Empty ? true : false; } }

        public string Apartment { get { return _resource.Apt; } set { _resource.Apt = value; } }

        public string City { get { return _resource.City; } set { _resource.City = value; } }

        public string State { get { return _resource.State; } set { _resource.State = value; } }

        public string Street { get { return _resource.Street; } set { _resource.Street = value; } }

        public string Zip { get { return _resource.Zip; } set { _resource.Zip = value; } }

        public object ResidentInformation {

            get {

                if ( _resource.PersonId == Guid.Empty && _resource.OutletId != Guid.Empty )
                    throw new ArgumentException ( "Address has no recorded resident!\n Person and Outlet Id's are null" );

                return _resource.PersonId != Guid.Empty ? ( object ) _resource.Person : ( object ) _resource.Outlet;

            } set {

                if ( _resource.PersonId == Guid.Empty && _resource.OutletId != Guid.Empty )
                    throw new ArgumentException ( "Address has no resident!\n Person and Outlet Id's are null" );

                if ( value.GetType ().FullName.Contains ( "Person" ) ) _resource.Person = ( Data.Entities.Person ) value;
                else if ( value.GetType ().FullName.Contains ( "Outlet" ) ) _resource.Outlet = ( Data.Entities.Outlet ) value;
                else throw new ArgumentException ( "Type assigned to Address.ResidentInformation instance is neither of type Person or Outlet" );

            }

        }

    }

}
