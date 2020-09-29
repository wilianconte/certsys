using CertSys.Data.SqlServer.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CertSys.Data.SqlServer.Repository
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();

            return services;
        }
    }
}
