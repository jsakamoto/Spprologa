using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Prolog;

namespace Spprologa
{
    public class SpprologaRuntime : ISpprologaRuntime
    {
        public IPrologEngine PrologEngine { get; private set; }

        private readonly HashSet<string> ConsultedResourceNames = new();

        public SpprologaRuntime(IPrologEngine prologEngine)
        {
            PrologEngine = prologEngine;
        }

        public void EnsureConsulted(ComponentBase razorComponent)
        {
            var targetType = razorComponent.GetType();
            var assembly = targetType.Assembly;
            var resName = targetType.FullName + ".razor.prolog";
            if (ConsultedResourceNames.Contains(resName)) return;
            ConsultedResourceNames.Add(resName);

            if (!assembly.GetManifestResourceNames().Contains(resName)) return;

            using var stream = assembly.GetManifestResourceStream(resName);
            if (stream == null) return;
            using var reader = new StreamReader(stream);
            var prologCode = reader.ReadToEnd();

            this.PrologEngine.ConsultFromString(prologCode);
        }

        public ISolutionCollection query(string query)
        {
            return this.PrologEngine.Query(query);
        }

        public EventCallback then(object receiver, string query)
        {
            return EventCallback.Factory.Create(receiver, () => this.PrologEngine.Query(query));
        }

        public bool solved(string query)
        {
            return this.query(query).Any();
        }

        public bool unsolved(string query)
        {
            return !this.query(query).Any();
        }
    }
}
