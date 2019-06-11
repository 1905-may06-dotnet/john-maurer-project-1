using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBox.Client.Models {

    //IGNORES: Items.Features

    public class OrderItemsModel {

        [ Display ( Name = "Item" ) ]
        public List < string > ItemName { get; set; }

    }

}
