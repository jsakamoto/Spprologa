using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Spprologa
{
    public interface ISpprologaRuntime
    {
        void EnsureConsulted(ComponentBase razorComponent);

        ISolutionCollection query(string query);

        Func<Task> then(string query);

        bool solved(string query);

        bool unsolved(string query);
    }
}
