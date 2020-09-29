using CerSys.Data.Model;
using CerSys.Domain;
using CertSys.Crosscutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CertSys.Calc
{
    class Program
    {
        #region Properties
        private static IConfigurationRoot Configuration { get; set; }
        private static IServiceCollection Service { get; set; }
        public static IServiceProvider Provider { get; set; }
        private static ICalcServices CalcServices { get; set; }
        #endregion

        #region Methods
        static void Main(string[] args)
        {
            //Load service provider
            LoadServiceProvider();

            bool sair = false;

            var configuration = CalcServices.GetLastConfiguration();

            while (!sair)
            {
                switch (LoadOptions())
                {
                    case "01":
                        {
                            CalDistanciaEntrePilares(configuration); 
                            break;
                        }
                    case "02":
                        {
                            CalDistanciaEntrePilaresComReforco(configuration);
                            break;
                        }
                    case "03":
                        {
                            sair = true;
                            break;
                        }
                }
            }
        }

        static void LoadServiceProvider()
        {
           //setup our appsettings
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

            //setup our DI
            Service = new ServiceCollection();

            //Configure injection
            IoC.Configure(Service, Configuration);

            //getProvider
            Provider = Service.BuildServiceProvider();

            //Get Service
            CalcServices = Service.BuildServiceProvider().GetService(typeof(ICalcServices)) as ICalcServices;
        }

        private static string LoadOptions()
        {
            Console.Clear();

            Console.WriteLine("Selecione a operação:");
            Console.WriteLine();

            var options = new string[]
            {
                "01 - Cálculo de distância máxima entre pilares",
                "02 - Cálculo para colocação de base reforçada",
                "03 - Sair",
            };

            foreach (var option in options)
            {
                Console.WriteLine(option);
            }

            return GetCMD(string.Empty);
        }

        private static void CalDistanciaEntrePilares(Configuration configuration)
        {

            double total;

            while (true)
            {
                if (!double.TryParse(GetCMD($"Informe o tamanho total ({configuration.MinTotal} min):"), out total))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Valor informado é inválido!");
                    Console.ReadLine();
                }
                else
                {
                    if (total < configuration.MinTotal)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Valor deve ser maior que {configuration.MinTotal}!");
                        Console.ReadLine();
                        continue;
                    }

                    break;
                }
            }

            double distancia;
            while (true)
            {
                if (!Double.TryParse(GetCMD($"Informe a distancia entre pilares (2 min) ({configuration.MaxVao} max):"), out distancia))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Valor informado é inválido!");
                    Console.ReadLine();
                }
                else
                {
                    if (distancia < 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Valor deve ser maior que 2 metros!");
                        Console.ReadLine();
                        continue;
                    }

                    if (distancia > configuration.MaxVao)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Valor deve ser menor que {configuration.MaxVao} metros!");
                        Console.ReadLine();
                        continue;
                    }

                    break;
                }
            }


            var calc = CalcServices.CalcDistMax(distancia, total);

            Console.WriteLine();
            Console.WriteLine($"A distancia entre pilares deve ser de {calc:0.00} metros");
            Console.WriteLine();
            Console.WriteLine($"----------------------------------------------");
            Console.ReadLine();
        }

        private static void CalDistanciaEntrePilaresComReforco(Configuration configuration)
        {

            double total;
            while (true)
            {
                if (!double.TryParse(GetCMD($"Informe o tamanho total ({configuration.MinTotal} min):"), out total))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Valor informado é inválido!");
                    Console.ReadLine();
                }
                else
                {
                    if (total < configuration.MinTotal)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Valor deve ser maior que {configuration.MinTotal}!");
                        Console.ReadLine();
                        continue;
                    }

                    break;
                }
            }

            double distancia;
            while (true)
            {
                if (!Double.TryParse(GetCMD($"Informe a distancia entre pilares (2 min) ({configuration.MaxVao} max):"), out distancia))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Valor informado é inválido!");
                    Console.ReadLine();
                }
                else
                {
                    if (distancia < 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Valor deve ser maior que 2 metros!");
                        Console.ReadLine();
                        continue;
                    }

                    if (distancia > configuration.MaxVao)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Valor deve ser menor que {configuration.MaxVao} metros!");
                        Console.ReadLine();
                        continue;
                    }

                    break;
                }
            }

            double reforco;
            while (true)
            {
                if (!double.TryParse(GetCMD($"Informe a distancia de reforço de base ({configuration.MaxBaseReforcada} max):"), out reforco))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Valor informado é inválido!");
                    Console.ReadLine();
                }
                else
                {

                    if (reforco < configuration.MaxBaseReforcada)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Valor deve ser maior que {configuration.MaxBaseReforcada} metros!");
                        Console.ReadLine();
                        continue;
                    }

                    break;
                }
            }

            var calc = CalcServices.CalcDistMax(distancia, total);
            var pilares = CalcServices.CalcReforcoBase(distancia, total, configuration.MaxBaseReforcada);

            Console.WriteLine();
            Console.WriteLine($"A distancia entre pilares deve ser de {calc:0.00} metros");
            Console.WriteLine($"os pilares {string.Join(", ", pilares.ToArray())} devem ser reforçados");
            Console.WriteLine();
            Console.WriteLine($"----------------------------------------------");
            Console.ReadLine();
        }

        private static string GetCMD(string question)
        {
            

            if (!string.IsNullOrEmpty(question))
            {
                Console.Clear();
                Console.WriteLine(question);
            }

            Console.WriteLine();

            Console.Write("CMD > ");

            return Console.ReadLine();

        }
        #endregion
    }
}