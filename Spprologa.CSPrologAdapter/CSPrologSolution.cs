using System.Linq;
using Prolog;

namespace Spprologa.CSProlog
{
    internal class CSPrologSolution : ISolution
    {
        private readonly PrologEngine.ISolution _Solution;

        public object? this[string varName] => GetVarVlaue(_Solution, varName);

        public CSPrologSolution(PrologEngine.ISolution solution)
        {
            _Solution = solution;
        }

        public override string? ToString()
        {
            var varvalue = _Solution.VarValuesIterator.FirstOrDefault(v => v.DataType != "namedvar");
            if (varvalue == null) return null;
            return varvalue.Value.ToString();
        }

        internal static object? GetVarVlaue(PrologEngine.ISolution solution, string varName)
        {
            var varvalue = solution.VarValuesIterator.FirstOrDefault(v => v.Name == varName);
            return GetVarValue(varvalue);
        }

        internal static object? GetVarValue(PrologEngine.IVarValue? varvalue)
        {
            if (varvalue == null) return null;
            if (varvalue.DataType == "namedvar") return null;

            static string trim(string str) => str.Substring(1, str.Length - 2);
            return varvalue.DataType switch
            {
                "string" => trim(varvalue.Value.ToString()),
                "number" => varvalue.Value.To<decimal>(),
                _ => varvalue.Value.ToString()
            };
        }
    }
}
