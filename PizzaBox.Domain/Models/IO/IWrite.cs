using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.IO {

    public abstract class IWrite < T > : IModel < T > where T: new () {

        public IWrite () : base () {}

        public abstract Elements.IElement < T > Save ();

        public abstract void Delete ();

    }

}
