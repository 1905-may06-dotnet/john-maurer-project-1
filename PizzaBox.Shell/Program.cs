using System;
using System.Linq;
using PizzaBox.Data;

namespace PizzaBox.Shell {

    class Program {

        static void Main ( string [] args ) {

            switch ( Utilities.MainMenu.MainPrompt () ) {

                case "1": { Utilities.MainMenu.CreateOutletCustomer (); } break;
                case "2": { Utilities.MainMenu.RemoveOutletCustomer (); } break;
                case "3": { Utilities.MainMenu.ViewOutletCustomers (); }  break;

                default: break;
                
            };

            

        }

    }

}
