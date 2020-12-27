using System;

namespace Spprologa
{
    public class FactBinder
    {
        private SpprologaComponentBase Component;

        public FactBinder(SpprologaComponentBase spprologaComponentBase)
        {
            this.Component = spprologaComponentBase;
        }

        public string this[string query]
        {
            get
            {
                Console.WriteLine($"factbinder[\"{query}\"].get()");
                if (this.Component.SpprologaRuntime == null) return "";
                var value = this.Component.query(string.Format(query, "X"));
                return value?.ToString() ?? "";
            }
            set
            {
                Console.WriteLine($"factbinder[\"{query}\"].set({value})");
                if (this.Component.SpprologaRuntime == null) return;
                this.Component.query("retractall(" + string.Format(query, "_").TrimEnd('.') + ").");
                this.Component.query("assert(" + string.Format(query, "'" + value + "'").TrimEnd('.') + ").");
            }
        }
    }
}
