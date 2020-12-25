using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Prolog;

namespace Spprologa
{
    public class SpprologaComponentBase : ComponentBase
    {
        [Inject]
        private PrologEngine? PrologEngine { get; set; }

        private static readonly HashSet<string> ConsultedResourceNames = new HashSet<string>();

        protected readonly FactBinder unifact;

        public SpprologaComponentBase()
        {
            unifact = new FactBinder();
        }

        private void EnsureConsulted()
        {
            if (this.PrologEngine == null) return;

            var thisType = this.GetType();
            var assembly = thisType.Assembly;
            var resName = thisType.FullName + ".razor.prolog";
            if (ConsultedResourceNames.Contains(resName)) return;
            ConsultedResourceNames.Add(resName);

            if (!assembly.GetManifestResourceNames().Contains(resName)) return;

            using var stream = assembly.GetManifestResourceStream(resName);
            if (stream == null) return;
            using var reader = new StreamReader(stream);
            var prologCode = reader.ReadToEnd();

            this.PrologEngine.ConsultFromString(prologCode);
        }

        protected string query(string query)
        {
            if (this.PrologEngine == null) throw new Exception();
            this.EnsureConsulted();

            this.PrologEngine.Query = query;
            var solution = this.PrologEngine.GetEnumerator().FirstOrDefault(sln => sln.Solved);
            if (solution == null) return "(nil)";

            //foreach (var varval in solution.VarValuesIterator)
            //{
            //    Console.WriteLine($"{varval.Name} = {varval.Value} ({varval.DataType})");
            //}
            var varvalue = solution.VarValuesIterator.FirstOrDefault(v => v.DataType != "namedvar");
            if (varvalue == null) return "(noval)";

            return varvalue.Value.ToString();// $"PrologEngine is: {PrologEngine != null}";
        }

        protected EventCallback then(string query)
        {
            if (this.PrologEngine == null) throw new Exception();
            this.EnsureConsulted();

            var prologEngine = this.PrologEngine;

            return EventCallback.Factory.Create(this, () =>
            {
                prologEngine.Query = query;
                prologEngine.GetEnumerator().FirstOrDefault();
            });
        }

    }
}
