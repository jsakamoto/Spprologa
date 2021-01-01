using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prolog;

namespace Spprologa
{
    public class Solutions : IEnumerable<Spprologa.Solution>
    {
        private readonly IEnumerator<PrologEngine.ISolution> _Enumarator;

        private readonly PrologEngine.ISolution? _FirstSolution;

        public Solutions(IEnumerable<PrologEngine.ISolution> solutions)
        {
            _Enumarator = solutions.Where(sln => sln.Solved).GetEnumerator();
            if (_Enumarator.MoveNext()) _FirstSolution = _Enumarator.Current;
        }

        public override string? ToString()
        {
            var solution = _FirstSolution;
            if (solution == null) return null;

            var varvalue = solution.VarValuesIterator.FirstOrDefault(v => v.DataType != "namedvar");
            if (varvalue == null) return null;

            return Solution.GetVarValue(varvalue)?.ToString();
        }

        public IEnumerator<Solution> GetEnumerator()
        {
            if (_FirstSolution == null) yield break;
            yield return new Solution(_FirstSolution);
            while (_Enumarator.MoveNext())
            {
                yield return new Solution(_Enumarator.Current);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public object? this[string varName]
        {
            get
            {
                var solution = _FirstSolution;
                if (solution == null) return null;
                return Solution.GetVarVlaue(solution, varName);
            }
        }
    }
}
