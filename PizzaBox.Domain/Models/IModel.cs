using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Models {

    public enum Status {

        Unchanged = 1,
        Updated   = 2,
        Deleted   = 3

    }

    public abstract class IModel < T > {

        protected T _resource;

        public Status HasChanged = Status.Unchanged;

        public IModel () { _resource = default; }

    }

}
