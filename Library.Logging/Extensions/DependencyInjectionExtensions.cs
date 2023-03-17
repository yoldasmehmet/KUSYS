using Library.Logging.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public static class LoggingDependencyInjectionExtensions
{

    public static void UseLogger(this IApplicationBuilder host)
    {


        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Library.Logging.EmbededResources.serilog.json";

        using (var configStream = new MemoryStream())
        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            string configTemplate = reader.ReadToEnd();
            var envirement = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration config;
            config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{envirement}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
            var logConfig = GetConfig(config); // config.GetSection("LogConfig").Get<LogConfig>();
            if (logConfig != null)
            {
                SetConsts(logConfig);
                configTemplate = configTemplate.Replace("{path}", logConfig.Path.Replace("\\", "\\\\"));
                configTemplate = configTemplate.Replace("{pathFormat}", logConfig.PathFormat.Replace("\\", "\\\\"));
                configTemplate = configTemplate.Replace("{port}", logConfig.Port);
                configTemplate = configTemplate.Replace("{server}", logConfig.Server.Replace("\\", "\\\\"));
                configTemplate = configTemplate.Replace("{applicationName}", logConfig.ApplicationName);
                configTemplate = configTemplate.Replace("{fileSizeLimitBytes}", logConfig.FileSizeLimitBytes.ToString());
                configTemplate = configTemplate.Replace("{retainedFileCountLimit}", logConfig.RetainedFileCountLimit.ToString());
                using (var writer = new StreamWriter(configStream))
                {
                    writer.Write(configTemplate);
                    writer.Flush();
                    configStream.Position = 0;

                    config = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json", false, true)
                   .AddJsonStream(configStream)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                   .AddEnvironmentVariables()
                   .Build();

                    Log.Logger = new LoggerConfiguration()
                           .ReadFrom.Configuration(config)
                           .Enrich.FromLogContext()
                           //.Enrich.WithExceptionDetails()
                           .Enrich.WithMachineName()
                           .CreateLogger();
                }
            }
        }
    }
    private static void SetConsts(LogConfig config)
    {  

        AppSettings.IsRegistered = true;
        AppSettings.PathFormat = config.PathFormat;
        AppSettings.Server = config.Server;
        AppSettings.Path = config.Path;
        AppSettings.Port = config.Port;
        AppSettings.ApplicationName = config.ApplicationName;
        AppSettings.IsLogSessionInfo = config.IsLogSessionInfo;
        AppSettings.RetainedFileCountLimit = config.RetainedFileCountLimit;
        AppSettings.FileSizeLimitBytes = config.FileSizeLimitBytes;
    }
    private static LogConfig GetConfig(IConfiguration env)
    {    
        //1073741824
        //fileSizeLimitBytes
        //retainedFileCountLimit
     

        var logConfig = env.GetSection("LogConfig").Get<LogConfig>();
        if (logConfig == null)
        {
            Console.Write(string.Concat( "Lütfen application.json dosyasına aşagıdaki konfigürasyonu giriniz." ,
                    Environment.NewLine,
            "\"LogConfig\": " , Environment.NewLine,
                    "{ " , Environment.NewLine,
                      "\"Server\": \"IP.IP.IP.IP\"", "//Gerekli kofigürasyonda gereken graylog server ip girilmesi gereklidir.(test-prod)", Environment.NewLine,
                      "\"Port\": \"3003\"", "//Kullanılacak serverin portu.",Environment.NewLine,
                      "\"Path\": \"C:\\Logs\\Uygulama_Adi\\log.txt\"" , Environment.NewLine,
                      "\"PathFormat\": \"C:\\Logs\\Uygulama_Adi\\log-{Date}.txt\"" , Environment.NewLine,
                      "\"ApplicationName\": \"Uygulama_Adi\"" , Environment.NewLine,
                      "\"FileSizeLimitBytes\":1073741824", Environment.NewLine,
                      "\"RetainedFileCountLimit\":3", Environment.NewLine,
                      "\"IsLogSessionInfo\": false", Environment.NewLine,
                    "}", Environment.NewLine
                    ));

        }

        return logConfig;
    }

}
