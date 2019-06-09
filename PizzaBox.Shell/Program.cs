using System;
using System.Linq;
using PizzaBox.Data;

namespace PizzaBox.Shell {

    class Program {

        static string choice_mainMenu;

        static void Main ( string [] args ) {

            do {

                choice_mainMenu = String.Empty;
                choice_mainMenu = Utilities.MainMenu.MainPrompt ();

                switch ( choice_mainMenu ) {

                    case "1": { Utilities.MainMenu.CreateOutletCustomer (); } break;
                    case "2": { Utilities.MainMenu.RemoveOutletCustomer (); } break;
                    case "3": { Utilities.MainMenu.ViewOutletCustomers  (); } break;

                    default: break;

                }

                if ( choice_mainMenu.ToLower () != "exit" ) {

                    Console.WriteLine ( "\n\nPress the enter key to return the main menu..." );
                    System.Console.ReadLine ();

                } 

            } while ( choice_mainMenu.ToLower () != "exit" );

            

        }

    }

}
