using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class CustomerQuery : EventArgs {

        public static new readonly CustomerQuery Empty = new CustomerQuery ();

        public Guid   Id    = Guid.Empty;
        public string Email = string.Empty;

        public CustomerQuery ( Guid Id ) { this.Id = Id; }

        public CustomerQuery ( string email ) { Email = email; }

        public CustomerQuery () { }

    }

    sealed public class Customer : IElement < Data.Entities.Person > {

        private static readonly object _cust_writeLock = new object ();

        public static readonly Customer Empty = new Customer ();

        public Customer () : base () {

            _resource = new Data.Entities.Person ();

            _resource.Id = Guid.NewGuid ();

        }

        public Customer ( Data.Entities.Person entity ) { _resource = entity; }

        public Customer ( Customer customer ) { _resource = customer._resource; }

        public override Elements.IElement < Data.Entities.Person > Save () {

            if ( _resource.Id == Guid.Empty || _resource.Id == null ) _resource.Id = Guid.NewGuid ();

            lock ( _cust_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( context.People.Find ( _resource.Id ) == null ) {

                        context.Attach < Data.Entities.Person > ( _resource );
                        context.Add < Data.Entities.Person > ( _resource );

                    } else context.Update < Data.Entities.Person > ( _resource );

                    context.SaveChanges ();

                }

            }

            return new Customer ( _resource );

        }

        public override void Delete () {

            lock ( _cust_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    context.Remove < Data.Entities.Person > ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public string FirstName { get { return _resource.Fname; } set { _resource.Fname = value; } }

        public string MiddleName { get { return _resource.Mname; } set { _resource.Mname = value; } }

        public string LastName { get { return _resource.Lname; } set { _resource.Lname = value; } }

        public string FormalName { get { return _resource.Lname + ", " + _resource.Fname + " " + _resource.Mname [ 0 ] + "."; } }

        public bool Gender { get { return ( bool ) _resource.Gender; } set { _resource.Gender = value; } }

        public DateTime? DoB { get { return _resource.DoB; } set { _resource.DoB = value; } }

        public string Phone { get { return _resource.Phone; } set { _resource.Phone = value; } }

        public string Email { get { return _resource.Email; } set { _resource.Email = value; } }

        public ICollection < Data.Entities.Address > Addresses {

            get { return _resource.Addresses; }
            set { _resource.Addresses = value; }

        }

        public ICollection < Data.Entities.Order > Orders {

            get { return _resource.Orders; }
            set { _resource.Orders = value; }

        }

    }

}
