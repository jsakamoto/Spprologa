using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prolog;

namespace Spprologa.DependencyInjection
{
    public static class SpprologaServiceExtension
    {
        public static IServiceCollection AddSpprologa(this IServiceCollection services)
        {
            services.TryAddScoped<SpprologaRuntime>();
            services.TryAddScoped(_ =>
            {
                var prologEngine = new PrologEngine(persistentCommandHistory: false);
                return prologEngine;
            });
            return services;
        }
    }
}
