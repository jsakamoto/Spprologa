using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Spprologa.DependencyInjection
{
    public static class SpprologaServiceExtension
    {
        public static ISpprologaServiceCollection AddSpprologa(this IServiceCollection services)
        {
            services.TryAddScoped<ISpprologaRuntime, SpprologaRuntime>();
            return new SpprologaServiceCollection(services);
        }
    }
}
