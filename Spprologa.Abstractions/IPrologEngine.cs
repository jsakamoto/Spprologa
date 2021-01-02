using Microsoft.AspNetCore.Components;

namespace Spprologa
{
    public interface IPrologEngine
    {
        void ConsultFromString(string prologCode);

        ISolutionCollection Query(string query);
    }
}
