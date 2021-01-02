using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Prolog;

namespace Spprologa.CSProlog
{
    internal class CSPrologSolutionCollection : ISolutionCollection
    {
        private const string ErrorMessagePattern = @"^[\r\n]*\*{3} (input string:|error:)";

        private readonly IEnumerator<PrologEngine.ISolution> _Enumarator;

        private readonly PrologEngine.ISolution? _FirstSolution;

        public CSPrologSolutionCollection(IEnumerable<PrologEngine.ISolution> solutions)
        {
            _Enumarator = solutions.GetEnumerator();
            while (_Enumarator.MoveNext())
            {
                var solution = _Enumarator.Current;
                var solutionText = solution.ToString() ?? "";
                if (Regex.IsMatch(solutionText, ErrorMessagePattern))
                {
                    var errMessageLines = solutionText.Split('\r', '\n')
                        .Select(t => t.Trim('*', ' ', '\t'))
                        .Where(t => !string.IsNullOrEmpty(t));
                    throw new PrologException(string.Join(Environment.NewLine, errMessageLines));
                }
                if (solution.Solved)
                {
                    _FirstSolution = solution;
                    break;
                }
            }
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
                if (_Enumarator.Current.Solved == false) continue;
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