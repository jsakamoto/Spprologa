using System.Threading.Tasks;

namespace Spprologa
{
    public interface IPrologEngine
    {
        void ConsultFromString(string prologCode);

        ISolutionCollection Query(string query);

        Task<ISolutionCollection> QueryAsync(string query);
    }
}
