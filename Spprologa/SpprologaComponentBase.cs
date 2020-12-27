﻿using System;
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

        private SpprologaRuntime GetSpprologaRuntime()
        {
            if (this.SpprologaRuntime == null) throw new Exception();
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

        protected Solutions query(string query) => this.GetSpprologaRuntime().query(query);

        protected EventCallback then(string query) => this.GetSpprologaRuntime().then(this, query);

        protected bool solved(string query) => this.GetSpprologaRuntime().solved(query);

        protected bool unsolved(string query) => this.GetSpprologaRuntime().unsolved(query);
    }
}
