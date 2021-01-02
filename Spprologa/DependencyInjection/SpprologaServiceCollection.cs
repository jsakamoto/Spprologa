using Microsoft.Extensions.DependencyInjection;

namespace Spprologa.DependencyInjection
{
    internal class SpprologaServiceCollection : ISpprologaServiceCollection
    {
        public IServiceCollection Services { get; }

        public SpprologaServiceCollection(IServiceCollection services)
        {
            Services = services;
        }
    }
}
