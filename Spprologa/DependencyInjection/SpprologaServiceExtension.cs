using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Spprologa.CSProlog;

namespace Spprologa.DependencyInjection
{
    public static class SpprologaServiceExtension
    {
        public static IServiceCollection AddSpprologa(this IServiceCollection services)
        {
            services.TryAddScoped<IPrologEngine, CSPrologEngineAdapter>();
            services.TryAddScoped<ISpprologaRuntime, SpprologaRuntime>();
            return services;
        }
    }
}
