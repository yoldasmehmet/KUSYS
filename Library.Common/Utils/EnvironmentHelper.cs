using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Common.Utils
{/// <summary>
/// Envirement yardımcı metodlarını içeren sınıf
/// </summary>
    public class EnvironmentHelper
    {
        static IConfigurationRoot config;
   
        public static IConfigurationRoot GetConfiguration()
        {

            
            return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true,
                        true)
                    .AddEnvironmentVariables()
                    .Build();
        }
        /// <summary>
        /// Hali hazırda işletilen envirementin içindeki sectionu alan metod.
        /// </summary>
        /// <param name="section">Alınacak section adı.</param>
        /// <returns></returns>
        public static IConfigurationSection GetSectionFromEnvirementVariables(string section)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (config == null)
            {
                config = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                  .AddJsonFile($"appsettings.{environment}.json", optional: true)
                  .Build();
            }
            var url = config.GetSection(section);
            return url;
        }
        public string EnvirementName()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }
    
    
        public static bool IsDeveleopment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }
        public static bool IsProduction()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
        }
        public static bool IsTest()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test";
        }

        public static object GetConfig()
        {
            throw new NotImplementedException();
        }
        public static T GetConfiguration<T>(string Section)
        {


            return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true,
                        true)
                    .AddEnvironmentVariables()
                    .Build().GetSection(Section).Get<T>();
        }
        public static string Value { get { return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); } }

    }

}
