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

        public Address () : base () {

            _resource    = new Data.Entities.Address ();
            _resource.Id = Guid.NewGuid ();

        }

        public Address ( Data.Entities.Address entity ) { _resource = entity; }

        public Address ( Address address ) { _resource = address._resource; }

        public override Elements.IElement < Data.Entities.Address > Save () {

            lock ( _addr_writeLock ) { 

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    var local = context.Addresses.Find ( _resource.Id );

                    if ( local == null ) {

                        context.Attach ( _resource );
                        context.Add < Data.Entities.Address > ( _resource );

                    } else {

                        local.Apt                 = _resource.Apt;
                        local.City                = _resource.City;
                        local.OrderId             = _resource.OrderId;
                        local.OutletId            = _resource.OutletId;
                        local.PersonId            = _resource.PersonId;
                        local.PersonId1           = _resource.PersonId1;
                        local.PersonId1Navigation = _resource.PersonId1Navigation;
                        local.State               = _resource.State;
                        local.Street              = _resource.Street;
                        local.Zip                 = _resource.Zip;

                        context.Update < Data.Entities.Address > ( local );

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

        public Guid? PersonId { get { return PersonId; } set { _resource.PersonId = value; } }

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

    }

}
