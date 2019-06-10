using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.Client.Models {

    public class LoginModel {

        [ Required ( ErrorMessage = "Please Provide Username", AllowEmptyStrings = false ) ]
        [ DisplayName ( "Username" ) ]
        public string Username { get; set; }

        [ Required ( ErrorMessage = "Please Provide Password", AllowEmptyStrings = false ) ]
        [ DataType ( DataType.Password ) ]
        [ DisplayName ( "Password" ) ]
        public string Password { get; set; }

    }

}
