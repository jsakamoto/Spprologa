using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prolog;

namespace Spprologa.CSProlog
{
    internal class CSPrologSolutionCollection : ISolutionCollection
    {
        private readonly IEnumerator<PrologEngine.ISolution> _Enumarator;

        private readonly PrologEngine.ISolution? _FirstSolution;

        public CSPrologSolutionCollection(IEnumerable<PrologEngine.ISolution> solutions)
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

            return CSPrologSolution.GetVarValue(varvalue)?.ToString();
        }

        public IEnumerator<ISolution> GetEnumerator()
        {
            if (_FirstSolution == null) yield break;
            yield return new CSPrologSolution(_FirstSolution);
            while (_Enumarator.MoveNext())
            {
                yield return new CSPrologSolution(_Enumarator.Current);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public object? this[string varName]
        {
            get
            {
                var solution = _FirstSolution;
                if (solution == null) return null;
                return CSPrologSolution.GetVarVlaue(solution, varName);
            }
        }
    }
}