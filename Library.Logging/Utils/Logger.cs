
using Libary.Logging.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Libary.Logging.Utils
{
    public enum LogType { Information, Error, Fatal, Warning }
    public class Logger
    {
        public static void Configure()
        {
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{environment}.json", optional: true)
            //    .Build();

            //Serilog.Log.Logger = new Serilog.LoggerConfiguration()
            //    .ReadFrom.Configuration(config)
            //    .CreateLogger();
        }
        public static void Log(string message, LogType logType, string title = "", [CallerMemberName] string method = "")
        {
            var assambly = Assembly.GetEntryAssembly();
            string sourceContext = $"{assambly.GetName().Name}_{title}";
            switch (logType)
            {
                case LogType.Information:
                    Serilog.Log.ForContext("SourceContext", sourceContext).ForContext("EventId", method).Information(message);
                    break;
                case LogType.Error:
                    Serilog.Log.ForContext("SourceContext", sourceContext).ForContext("EventId", method).Error(message);
                    break;
                case LogType.Fatal:
                    Serilog.Log.ForContext("SourceContext", sourceContext).ForContext("EventId", method).Fatal(message);
                    break;
                case LogType.Warning:
                    Serilog.Log.ForContext("SourceContext", sourceContext).ForContext("EventId", method).Warning(message);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// İçerisinde çalıştığı method ve tüm bilgilerinin loglanmasını sağlar.
        /// Development aşamasında bulunmayan hataların bulunmasında kullanılması uygundur.
        /// </summary>
        /// <param name="message"></param>
        public static void LogTrace(string message)
        {
            var trace = (new System.Diagnostics.StackTrace()).GetFrame(1);
            var method = trace.GetMethod();
            var methodName=  method.Name;
            var declaringType = method.DeclaringType.Name;
            var reflectedType = method.ReflectedType.Name;
            var module = method.Module.Name;
            var methodBody = method.GetMethodBody().ToJson();
            Log(message, LogType.Information, $"methodName:{methodName} ### declaringType:{declaringType}### reflectedType:{reflectedType}### module:{module}### methodBody:{methodBody}");
        }
        public static void LogError(string message, string title = "")
        {
            Log(message, LogType.Error, title);
        }
        public static void LogInformation(string message, string title = "")
        {
            Log(message, LogType.Information, title);
        }
        public static void LogFatal(string message, string title = "")
        {
            Log(message, LogType.Fatal, title);
        }
        public static void LogWarning(string message, string title = "")
        {
            Log(message, LogType.Warning, title);
        }
        public static void CloseAndFlush()
        {
            Serilog.Log.CloseAndFlush();
        }
    }
}
