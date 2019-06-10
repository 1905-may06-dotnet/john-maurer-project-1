using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PizzaBox.Client.Controllers {

    public class EmployeeController : Controller {

        public IActionResult Index () {

            if ( LoginController.Token == Guid.Empty ) return RedirectToAction ( "Index", "Login" );
            else return View ();

        }

    }

}