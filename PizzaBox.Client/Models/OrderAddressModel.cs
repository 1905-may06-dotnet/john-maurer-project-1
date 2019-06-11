using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBox.Client.Models {

    public class OrderAddressModel {
        [ Required ]
        [ DisplayName ( "State" ) ]
        public List < string > State { get; }

        [ Required ]
        [ DisplayName ( "City" ) ]
        public string City { get; set; }

        [ Required ]
        [ DisplayName ( "Street" ) ]
        public string Street { get; set; }

        [ Required ]
        [ DisplayName ( "Zip" ) ]
        public string Zip { get; set; }

        [ DisplayName ( "Apartment/Suite" ) ]
        public string Apartment { get; set; }

        [ Required ]
        [ DisplayName ( "Items" ) ]
        public string Items { get; set; }

    }

}
