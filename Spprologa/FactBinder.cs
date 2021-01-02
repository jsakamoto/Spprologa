using System.Linq;

namespace Spprologa
{
    public class FactBinder
    {
        public string? as_atom
        {
            get => this.Get()?.Trim('\'');
            set => this.Set("\'" + (value ?? "") + "\'");
        }

        public string? as_string
        {
            get => this.Get();
            set => this.Set("\"" + (value ?? "") + "\"");
        }

        public int? as_int
        {
            get => int.TryParse(this.Get(), out var n) ? n : null;
            set => this.Set(value.HasValue ? value.Value.ToString() : "null");
        }

        private readonly ISpprologaRuntime _Runtime;

        private readonly string _Query;

        public FactBinder(ISpprologaRuntime runtime, string query)
        {
            this._Runtime = runtime;
            this._Query = query;
        }

        private string? Get()
        {
            return this._Runtime.query(string.Format(_Query, "X")).ToString();
        }

        private void Set(string value)
        {
            this._Runtime.query("retractall(" + string.Format(this._Query, "_").TrimEnd('.') + ").").FirstOrDefault();
            this._Runtime.query("asserta(" + string.Format(this._Query, value).TrimEnd('.') + ").").FirstOrDefault();
        }
    }
}
