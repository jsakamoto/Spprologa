using Microsoft.Extensions.DependencyInjection;

namespace Spprologa
{
    public interface ISpprologaServiceCollection
    {
        IServiceCollection Services { get; }
    }
}
