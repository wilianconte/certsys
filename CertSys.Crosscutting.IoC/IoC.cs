using CerSys.Domain.Extentions;
using CertSys.Data.MSSQL.Context;
using CertSys.Data.MSSQL.Context.Interfaces;
using CertSys.Data.SqlServer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CertSys.Crosscutting.IoC
{
    public static class IoC
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {

            //CertSys.Data.SqlServer.Context
            services.AddSingleton<ISqlServerContext>(p => new SqlServerContext(configuration["Settings:ConString"]));

            //CertSys.Data.SqlServer.Repository
            services.AddRepositories();

            //CertSys.Domain
            services.AddBusinessServices();
        }
    }
}
