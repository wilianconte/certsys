using Microsoft.Extensions.DependencyInjection;

namespace CerSys.Domain.Extentions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<ICalcServices, CalcServices>();

            return services;
        }
    }
}
