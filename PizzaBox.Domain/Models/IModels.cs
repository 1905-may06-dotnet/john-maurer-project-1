using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models {

    public abstract class IModels < T, Ty > : IO.IRead < T, Ty >
        where T: new ()
        where Ty: EventArgs, new () {

        public IModels () {}

        abstract public T Query ( ref Ty Index );

        public HashSet < T > Records { get { return ReadAll (); } }

    }

}
