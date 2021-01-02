using Microsoft.Extensions.DependencyInjection.Extensions;
using Spprologa.CSProlog;

namespace Spprologa.DependencyInjection
{
    public static class SpprologaCSPrologServiceExtension
    {
        public static ISpprologaServiceCollection UseCSProlog(this ISpprologaServiceCollection services)
        {
            services.Services.TryAddScoped<IPrologEngine, CSPrologEngineAdapter>();
            return services;
        }
    }
}
