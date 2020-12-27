using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prolog;

namespace Spprologa
{
    public class Solutions : IEnumerable<Spprologa.Solution>
    {
        private readonly IEnumerable<PrologEngine.ISolution> _Solutions;

        public Solutions(IEnumerable<PrologEngine.ISolution> solutions)
        {
            _Solutions = solutions;
        }

        public override string? ToString()
        {
            var solution = _Solutions.FirstOrDefault(sln => sln.Solved);
            if (solution == null) return null;

            var varvalue = solution.VarValuesIterator.FirstOrDefault(v => v.DataType != "namedvar");
            if (varvalue == null) return null;

            return Solution.GetVarValue(varvalue)?.ToString();
        }

        public IEnumerator<Solution> GetEnumerator()
        {
            foreach (var solution in _Solutions.Where(sln => sln.Solved))
            {
                yield return new Solution(solution);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public object? this[string varName]
        {
            get
            {
                var solution = _Solutions.FirstOrDefault(sln => sln.Solved);
                if (solution == null) return null;
                return Solution.GetVarVlaue(solution, varName);
            }
        }
    }
}
