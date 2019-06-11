using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Client.Models;

namespace PizzaBox.Client.Controllers {

    public class HomeController : Controller {

        public HomeController () {

            ViewBag.Items  = GetItems ();
            ViewBag.States = GetStates ();

        }

        private List < string > GetStates () {

            var result = new List < string > ();

            foreach ( var element in new PizzaBox.Domain.Models.Taxes ().Records ) result.Add ( element.Territory );

            return result;

        }

        private List < string > GetItems () {

            var result = new List < string > ();

            foreach ( var element in new PizzaBox.Domain.Models.Products ().Records ) result.Add ( element.Name );

            return result;

        }

        /*private List < string > GetItems () {

            var result = new List < string > ();

            foreach ( var element in new PizzaBox.Domain.Models.Products ().Records )
                result.Add ( "<option value=\"" + element.Name + "\">" + element.Name + "</option>" );

            return result;

        }*/

        private List < double > GetPrices () {

            var result = new List < double > ();

            foreach ( var element in new PizzaBox.Domain.Models.Products ().Records ) result.Add ( element.Price );

            return result;

        }

        //[ RequireHttps ]
        //[ HttpPost ]
        [ Route ( "PushLocation" ) ]
        public /*IActionResult*/ void PushLocation () {

            //return View ();

        }

        //[ RequireHttps ]
        //[ HttpPost ]
        [ Route ( "PushRecipient" ) ]
        public /*IActionResult*/ void PushRecipient () {

            //return View ();

        }

        //[ RequireHttps ]
        //[ HttpPost ]
        [ Route ( "PushItems" ) ]
        public /*IActionResult*/ void PushItems () {

            //return View ();

        }

        [ RequireHttps ]
        [ HttpPost ]
        [ Route ( "PrintReciept" ) ]
        public IActionResult PrintReciept () {

            return View ();

        }

        public IActionResult Index () { return View (); }

        public IActionResult Privacy () { return View (); }

        [ ResponseCache ( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true ) ]
        public IActionResult Error () {

            return View ( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );

        }

    }

}
