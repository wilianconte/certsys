using CertSys.Crosscutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows.Forms;

namespace CertSys
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            //setup our appsettings
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var services = new ServiceCollection();

            ConfigureServices(services, configuration);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var CalcPilarFrm = serviceProvider.GetRequiredService<CalcPilarFrm>();

                Application.Run(CalcPilarFrm);
            }
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CalcPilarFrm>();

            IoC.Configure(services, configuration);
        }
    }
}
