using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    sealed public class EmployeeQuery : EventArgs {

        public static new readonly EmployeeQuery Empty = new EmployeeQuery ();

        public Guid   Id       = Guid.Empty;
        public string Username = string.Empty;
        public string Password = string.Empty;

        public EmployeeQuery () { }

        public EmployeeQuery ( Guid Id ) { if ( Id != Guid.Empty ) this.Id = Id; }

        public EmployeeQuery ( string username, string password ) { Username = username; Password = password; }

        public EmployeeQuery ( string username ) { Username = username; }

    }

    sealed public class Employee : IElement < Data.Entities.Employee > {

        private static readonly object _emp_writeLock = new object ();

        public static readonly Employee Empty = new Employee();

        public Employee () : base () {

            _resource = new Data.Entities.Employee ();

            _resource.Id = Guid.NewGuid ();

        }

        public Employee ( Data.Entities.Employee entity ) { _resource = entity; }

        public Employee ( Employee employee ) { _resource = employee._resource; }

        public override Elements.IElement < Data.Entities.Employee > Save () {

            if ( _resource.Id == Guid.Empty || _resource.Id == null ) _resource.Id = Guid.NewGuid ();

            lock ( _emp_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    if ( context.Employees.Find ( _resource.Id ) == null ) {

                        context.Attach < Data.Entities.Employee > ( _resource );
                        context.Add < Data.Entities.Employee > ( _resource );

                    } else context.Update < Data.Entities.Employee > ( _resource );

                    context.SaveChanges ();

                }

            }

            return new Employee ( _resource );

        }

        public override void Delete () {

            lock ( _emp_writeLock ) {

                using ( var context = new Data.PizzaBoxDbContext () ) {

                    context.Remove < Data.Entities.Employee > ( _resource );
                    context.SaveChanges ();

                }

            }

        }

        public override bool Uncommitted { get { return _resource.Id == Guid.Empty ? true : false; } }

        public override Guid Id { get { return _resource.Id; } }

        public Guid PersonId { get { return _resource.PersonId; } set { _resource.PersonId = value; } }

        public Guid? EmployerId { get { return _resource.OutletId; } set { _resource.OutletId = value; } }

        public Data.Entities.Person Information { get { return _resource.Person; } set { _resource.Person = value; } }

        public Data.Entities.Outlet Employer { get { return _resource.Outlet; } set { _resource.Outlet = value; } }

        public string Username { get { return _resource.Username; } set { _resource.Username = value; } }

        public string Password { get { return _resource.Password; } set { _resource.Password = value; } }

        public string Status { get { return _resource.Status; } set { _resource.Status = value; } }

        public string Position { get { return _resource.Position; } set { _resource.Position = value; } }

        public double Wage { get { return _resource.Wage; } set { _resource.Wage = value; } }

        public string WageType { get { return _resource.WageType; } set { _resource.WageType = value; } }

    }

}
