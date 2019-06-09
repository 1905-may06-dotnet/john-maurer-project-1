using System;
using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Shell.Utilities {

    sealed public class MainMenu {

        private static List < string > prompts = new List< string > ();

        private static bool MainPromptIsValid ( string selection ) {

            return ( selection == "1" || selection == "2" || selection == "3" || selection.ToLower () == "exit"  );

        }

        private static void Banner () {

            Console.Clear ();

            Console.WriteLine ( "PizzaBox.Shell - Outlet Manager\n\n" );

        }

        public static void CreateOutletCustomer () {

            Utilities.MainMenu.Banner ();

            var prompts = new List < string > ();

            var business  = new Domain.Models.Elements.Business ();
            var bizaddr   = new Domain.Models.Elements.Address  ();
            var owner     = new Domain.Models.Elements.Employee ();
            var owneraddr = new Domain.Models.Elements.Address  ();

            var birthday   = 0;
            var birthmonth = 0;
            var birthyear  = 0;

            prompts.Add ( "Company Name: " );

            prompts.Add ( "        City: " );
            prompts.Add ( "       State: " );
            prompts.Add ( "      Street: " );
            prompts.Add ( "         Zip: " );
            prompts.Add ( "       Suite: " );

            prompts.Add ( "  First Name: " );
            prompts.Add ( " Middle Name: " );
            prompts.Add ( "   Last Name: " );
            prompts.Add ( "       Email: " );
            prompts.Add ( "       Phone: " );
            prompts.Add ( "   Birth Day: " );
            prompts.Add ( " Birth Month: " );
            prompts.Add ( "  Birth Year: " );

            prompts.Add ( "        City: " );
            prompts.Add ( "       State: " );
            prompts.Add ( "      Street: " );
            prompts.Add ( "         Zip: " );
            prompts.Add ( "       Suite: " );

            prompts.Add ( "    Username: " );
            prompts.Add ( "    Password: " );
            prompts.Add ( "        Wage: " );
            prompts.Add ( "    WageType: " );
            prompts.Add ( "      Gender: " );

            owner.Position = "owner";
            owner.Status   = "active";
            owner.WageType = "salary";
            owner.Information = new Data.Entities.Person ();

            Console.WriteLine ( "Create Outlet\n" );

            Console.Write ( prompts [ 0 ] );
            business.Name = Console.ReadLine ();

            Banner ();

            Console.WriteLine ( "Outlet Location - {0}", business.Name );

            Console.WriteLine ();
            Console.Write ( prompts [ 1 ] );
            bizaddr.City = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 2 ] );
            bizaddr.State = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 3 ] );
            bizaddr.Street = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 4 ] );
            bizaddr.Zip = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 5 ] );
            bizaddr.Apartment = Console.ReadLine ();

            Banner ();

            Console.WriteLine ( "Owner Personal Information - {0}", business.Name );

            Console.WriteLine ();
            Console.Write ( prompts [ 6 ] );
            owner.Information.Fname = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 7 ] );
            owner.Information.Mname = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 8 ] );
            owner.Information.Lname = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 9 ] );
            owner.Information.Email = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 10 ] );
            owner.Information.Phone = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 11 ] );
            birthday = Convert.ToInt32 ( Console.ReadLine () );
            Console.WriteLine ();
            Console.Write ( prompts [ 12 ] );
            birthmonth = Convert.ToInt32 ( Console.ReadLine () );
            Console.WriteLine ();
            Console.Write ( prompts [ 13 ] );
            birthyear = Convert.ToInt32 ( Console.ReadLine () );

            Banner ();

            Console.WriteLine ( "Owner Personal Information - Location - {0}", business.Name );

            Console.WriteLine ();
            Console.Write ( prompts [ 14 ] );
            owneraddr.City = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 15 ] );
            owneraddr.State = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 16 ] );
            owneraddr.Street = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 17 ] );
            owneraddr.Zip = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 18 ] );
            owneraddr.Apartment = Console.ReadLine ();

            Banner ();

            Console.WriteLine ( "Owner Personal Information - Profile - {0}", business.Name );

            Console.WriteLine ();
            Console.Write ( prompts [ 19 ] );
            owner.Username = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 20 ] );
            owner.Password = Console.ReadLine ();
            Console.WriteLine ();
            Console.Write ( prompts [ 21 ] );
            owner.Wage = Convert.ToDouble ( Console.ReadLine () );
            Console.WriteLine ();
            Console.Write ( prompts [ 22 ] );
            owner.Information.Gender = Console.ReadLine ().ToLower () == "male" ? false : true;

            owner.Information.DoB = new DateTime ( birthyear, birthmonth, birthday );

            business.Save ();

            bizaddr.OutletId = business.Id;

            bizaddr.Save ();

            new Domain.Models.Elements.Customer ( owner.Information ).Save ();

            owner.EmployerId = business.Id;

            owner.Save ();

            owneraddr.PersonId = owner.PersonId;
            owneraddr.OutletId = business.Id;
            
            owneraddr.Save ();

        }

        public static void RemoveOutletCustomer () {

            var businesses = new Businesses ();

            var records      = businesses.Records;
            var index        = string.Empty;
            var confirmation = "Name not Found";

            Banner ();

            Console.WriteLine ( "Remove Outlet\n" );
            Console.WriteLine ( "Type the name of the Outlet to Delete, press enter to continue\n" );

            foreach ( var record in records )
                Console.WriteLine ( "Company Name: " + record.Name );

            Console.Write ( "\nSelection: " );

            index = Console.ReadLine ();

            foreach ( var record in records ) {

                if ( record.Name.ToLower () == index.ToLower () ) {

                    var items     = new Products ();
                    var orders    = new Orders ();
                    var employees = new Employees ();
                    
                    record.Delete ();

                    foreach ( var element in items.Records ) if ( element.OutletId == null )
                        new Domain.Models.Elements.Product ( element ).Delete ();

                    foreach ( var element in orders.Records ) if ( element.OutletId == null )
                        new Domain.Models.Elements.Order ( element ).Delete ();

                    foreach ( var element in employees.Records ) if ( element.EmployerId == null )
                        new Domain.Models.Elements.Employee ( element ).Delete ();

                    confirmation = "Outlet record has been deleted, press enter to continue";

                }

            }

            Console.WriteLine ( confirmation );
            Console.ReadLine ();

            MainPrompt ();

        }

        public static void ViewOutletCustomers () {

            var businesses = new Businesses ();

            Banner ();

            Console.WriteLine ( "View all Outlets\n" );

            foreach ( var business in businesses.Records ) Console.WriteLine ( "Company Name: " + business.Name );

        }

        public static string MainPrompt () {

            string selection;

            do { 

                Banner ();

                Console.Write ( "1) Create Outlet\n2) Remove Outlet\n3) View Outlets\n\nEnter, 'exit', to close shell\n\nSelection:  " );
                selection = Console.ReadLine ();

                Console.Clear ();

            } while ( ! MainPromptIsValid ( selection ) );

            return selection;

        }



    }

}
