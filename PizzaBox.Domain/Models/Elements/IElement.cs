using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models.Elements {

    public class IElement < T > : IO.IWrite < T > where T : new () {

        public IElement () : base () {}

        public virtual bool Uncommitted { get; }

        public virtual Guid Id { get; }

        public override void Delete () { throw new NotImplementedException (); }

        public override Elements.IElement < T > Save () { throw new NotImplementedException (); }

    }

}
