using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.Client.Models {

    //IGNORES: Person.Gender

    public class OrderRecipientModel {

        [ Required ]
        [ DisplayName ( "First Name" ) ]
        public string FirstName { get; set; }

        [ Required ]
        [ DisplayName ( "Middle Name" ) ]
        public string MiddleName { get; set; }

        [ Required ]
        [ DisplayName ( "Last Name" ) ]
        public string LastName { get; set; }

        [ Required ]
        [ DisplayName ( "Birth day" ) ]
        public string Birthday { get; set; }

        [ Required ]
        [ DisplayName ( "Birth Month" ) ]
        public string BirthMonth { get; set; }

        [ Required ]
        [ DisplayName ( "Birth Year" ) ]
        public string BirthYear { get; set; }

        [ Required ]
        [ DisplayName ( "Phone Number" ) ]
        public string PhoneNumber { get; set; }

        [ DisplayName ( "Email" ) ]
        [ DataType ( DataType.EmailAddress ) ]
        public string Email { get; set; }

    }

}
