using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Spprologa
{
    public class SpprologaComponentBase : ComponentBase
    {
        [Inject]
        private SpprologaRuntime? SpprologaRuntime { get; set; }

        private readonly Dictionary<string, FactBinder> _FactBindersCache = new();

        public SpprologaComponentBase()
        {
        }

        public FactBinder fact(string query)
        {
            if (this.SpprologaRuntime == null) throw new Exception();
            this.SpprologaRuntime.EnsureConsulted(this);
            if (!_FactBindersCache.TryGetValue(query, out var binder))
            {
                _FactBindersCache.Add(query, binder = new FactBinder(this.SpprologaRuntime, query));
            }
            return binder;
        }

        protected Solutions query(string query)
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
