using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Domain.Models;

namespace PizzaBox.Shell.Utilities {

    sealed public class MainMenu {

        private static List < string > prompts = new List< string > ();

        private static bool MainPromptIsValid ( string selection ) {

            return ( selection == "1" || selection == "2" || selection == "3" || selection.ToLower () == "exit"  );

        }

        private static void Banner () {

            System.Console.Clear ();

            System.Console.WriteLine ( "PizzaBox.Shell - Outlet Manager\n\n" );

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

            System.Console.WriteLine ( "Create Outlet\n\n" );

            System.Console.WriteLine ( prompts [ 0 ] );
            business.Name = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 1 ] );
            bizaddr.City = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 2 ] );
            bizaddr.State = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 3 ] );
            bizaddr.Street = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 4 ] );
            bizaddr.Zip = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 5 ] );
            bizaddr.Apartment = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 6 ] );
            owner.Information.Fname = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 7 ] );
            owner.Information.Mname = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 8 ] );
            owner.Information.Lname = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 9 ] );
            owner.Information.Email = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 10 ] );
            owner.Information.Phone = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 11 ] );
            birthday = Convert.ToInt32 ( System.Console.ReadLine () );
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 12 ] );
            birthmonth = Convert.ToInt32 ( System.Console.ReadLine () );
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 13 ] );
            birthyear = Convert.ToInt32 ( System.Console.ReadLine () );
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 14 ] );
            owneraddr.City = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 15 ] );
            owneraddr.State = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 16 ] );
            owneraddr.Street = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 17 ] );
            owneraddr.Zip = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 18 ] );
            owneraddr.Apartment = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 19 ] );
            owner.Username = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 20 ] );
            owner.Password = System.Console.ReadLine ();
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 21 ] );
            owner.Wage = Convert.ToDouble ( System.Console.ReadLine () );
            System.Console.WriteLine ();
            System.Console.WriteLine ( prompts [ 22 ] );
            owner.Information.Gender = System.Console.ReadLine ().ToLower () == "male" ? false : true;

            owner.Information.DoB = new DateTime ( birthyear, birthmonth, birthday );

            business.Save ();
            bizaddr.OutletId = business.Id;
            bizaddr.Save ();
            owner.EmployerId = business.Id;
            
            new PizzaBox.Domain.Models.Elements.Customer ( owner.Information ).Save ();

            owner.PersonId = owner.Information.Id;

            owner.Save ();
            owneraddr.PersonId = owner.PersonId;
            owneraddr.Save ();

            System.Console.WriteLine ();
            System.Console.WriteLine ( "saved..." );

            MainPrompt ();

        }

        public static void RemoveOutletCustomer () {

            Utilities.MainMenu.Banner ();

            System.Console.WriteLine ( "Remove Outlet" );
            System.Console.ReadLine ();

            MainPrompt ();

        }

        public static void ViewOutletCustomers () {

            Utilities.MainMenu.Banner ();

            System.Console.WriteLine ( "View all Outlets" );
            System.Console.ReadLine ();

            MainPrompt ();

        }

        public static string MainPrompt () {

            string selection;

            do { 

                Utilities.MainMenu.Banner ();

                System.Console.WriteLine ( "1) Create Outlet\n2) Remove Outlet\n3) View Outlets\n\nEnter, 'exit', to close shell\n\nSelection - " );
                selection = System.Console.ReadLine ();

                System.Console.Clear ();

            } while ( ! MainPromptIsValid ( selection ) );

            return selection;

        }



    }

}
