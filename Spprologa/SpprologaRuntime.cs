using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Prolog;

namespace Spprologa
{
    public class SpprologaRuntime
    {
        public PrologEngine PrologEngine { get; private set; }

        private readonly HashSet<string> ConsultedResourceNames = new();

        public SpprologaRuntime(PrologEngine prologEngine)
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

        public Solutions query(string query)
        {
            this.PrologEngine.Query = query;
            return new Solutions(this.PrologEngine.GetEnumerator());
        }

        public EventCallback then(object receiver, string query)
        {
            var prologEngine = this.PrologEngine;
            return EventCallback.Factory.Create(receiver, () =>
            {
                prologEngine.Query = query;
                prologEngine.GetEnumerator().FirstOrDefault();
            });
        }
    }
}
