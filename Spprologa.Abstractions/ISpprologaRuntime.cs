using Microsoft.AspNetCore.Components;

namespace Spprologa
{
    public interface ISpprologaRuntime
    {
        void EnsureConsulted(ComponentBase razorComponent);

        ISolutionCollection query(string query);

        EventCallback then(object receiver, string query);

        bool solved(string query);

        bool unsolved(string query);
    }
}
