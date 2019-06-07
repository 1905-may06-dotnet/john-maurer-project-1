using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaBox.Domain.Models.IO {

    public abstract class IRead < T, Ty > : IModel < HashSet < T > >
        where T: new ()
        where Ty: EventArgs, new () {

        protected abstract T Read ( Ty entityArgs );

        public IRead () : base () {}

        public abstract HashSet < T > ReadAll ();

    }

}
