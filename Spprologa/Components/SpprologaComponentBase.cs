using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Spprologa.Components
{
    public class SpprologaComponentBase : ComponentBase
    {
        [Inject]
        private ISpprologaRuntime? SpprologaRuntime { get; set; }

        private readonly Dictionary<string, FactBinder> _FactBindersCache = new();

        protected override void OnInitialized()
        {
            GetSpprologaRuntime();
        }

        protected override Task OnInitializedAsync()
        {
            GetSpprologaRuntime();
            return Task.CompletedTask;
        }

        private ISpprologaRuntime GetSpprologaRuntime()
        {
            if (this.SpprologaRuntime == null) throw new InvalidOperationException("SpprologaRuntime is not injected yet.");
            this.SpprologaRuntime.EnsureConsulted(this);
            return this.SpprologaRuntime;
        }

        public FactBinder fact(string query)
        {
            if (!_FactBindersCache.TryGetValue(query, out var binder))
            {
                _FactBindersCache.Add(query, binder = new FactBinder(this.GetSpprologaRuntime(), query));
            }
            return binder;
        }

        protected ISolutionCollection query(string query) => this.GetSpprologaRuntime().query(query);

        protected EventCallback then(string query) => this.GetSpprologaRuntime().then(this, query);

        protected bool solved(string query) => this.GetSpprologaRuntime().solved(query);

        protected bool unsolved(string query) => this.GetSpprologaRuntime().unsolved(query);
    }
}
