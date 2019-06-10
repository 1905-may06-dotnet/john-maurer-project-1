using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace PizzaBox.Client.Controllers {

    public class LoginController : Controller {

        public static Guid Token = Guid.Empty;

        private IActionResult Authorize () {

            //PROTOTYPE CODE: Avoids the labor of managing multiple businesses; requires existing outlet record (**use PizzaBox.Shell)
            Token = new Domain.Models.Businesses ().Records.FirstOrDefault ().Id;

            return RedirectToAction ( "Index", "Employee" );

        }

        [ RequireHttps ]
        public IActionResult Index () { return View (); }

        [ Route ( "Login/SignIn" ) ]
        [ RequireHttps ]
        [ HttpPost ]
        public IActionResult SignIn ( string username, string password ) {

            var query     = new Domain.Models.Elements.EmployeeQuery { Username = username, Password = password };
            var employee  = new Domain.Models.Employees ().Query ( ref query );

            return password == null || employee == Domain.Models.Elements.Employee.Empty
                ? View ( "SignIn" )
                : Authorize ();

        }

        public IActionResult Retry () { return View (); }

    }

}