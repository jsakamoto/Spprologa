using System;
using Microsoft.AspNetCore.Components;

namespace Spprologa
{
    public class SpprologaComponentBase : ComponentBase
    {
        [Inject]
        internal SpprologaRuntime? SpprologaRuntime { get; private set; }

        protected readonly FactBinder unifact;

        public SpprologaComponentBase()
        {
            unifact = new FactBinder(this);
        }

        internal protected Solutions query(string query)
        {
            if (this.SpprologaRuntime == null) throw new Exception();
            this.SpprologaRuntime.EnsureConsulted(this);
            return this.SpprologaRuntime.query(query);
        }

        protected EventCallback then(string query)
        {
            if (this.SpprologaRuntime == null) throw new Exception();
            this.SpprologaRuntime.EnsureConsulted(this);
            return this.SpprologaRuntime.then(this, query);
        }
    }
}
