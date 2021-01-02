using System.Collections.Generic;

namespace Spprologa
{
    public interface ISolutionCollection : IEnumerable<ISolution>
    {
        object? this[string varName] { get; }

        string? ToString();
    }
}
