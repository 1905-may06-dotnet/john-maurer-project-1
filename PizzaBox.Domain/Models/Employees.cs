using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBox.Domain.Models {

    public class Employees : IModels < Elements.Employee, Elements.EmployeeQuery > {

        protected override Elements.Employee Read ( Elements.EmployeeQuery entityArgs ) {

            using ( var context = new Data.PizzaBoxDbContext () ) {

                if ( entityArgs.Id != null && entityArgs.Id != Guid.Empty )
                    return new Elements.Employee ( context.Employees.Find ( entityArgs.Id ) );
                else if ( entityArgs.Username != null && entityArgs.Username != string.Empty )
                    return new Elements.Employee 
                        ( ( from rec in context.Employees where rec.Username == entityArgs.Username select rec ).FirstOrDefault () );
                else return Elements.Employee.Empty;

            }

        }

        protected override HashSet < Elements.Employee > ReadAll () {

            using ( var context = new Data.PizzaBoxDbContext () ) { 

                var result = new HashSet < Elements.Employee > ();

                foreach ( var record in context.Employees ) 
                    result.Add ( new Elements.Employee ( record ) );

                return result;

            }

        }

        public Employees () : base () {}

        public override Elements.Employee Query ( ref Elements.EmployeeQuery Index ) { return Read ( Index ); }

    }

 }
