using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logging.Consts
{
    public class AppSettings
    {
        public static string Server { get; internal set; }

        public static string PathFormat { get; internal set; }
        public static string Port { get; internal set; }
        public static string Path { get; internal set; }
        public static string ApplicationName { get; internal set; }
        public static bool IsLogSessionInfo { get; internal set; }
        public static int FileSizeLimitBytes { get; set; }
        public static int RetainedFileCountLimit { get; set; }
        internal static bool IsRegistered { get; set; }

        internal static void CheckIsRegistered()
        {
            if (!IsRegistered)
            {
                var message = "Startup.cs'de app.UseLogger() kullanmalısınız.";
                Console.WriteLine(message);
                //throw new Exception(message);
            }
        }
    }
}
