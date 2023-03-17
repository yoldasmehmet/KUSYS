using Library.Common.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Security
{
    public class AppSettings:ApplicationSettingsBase
    {
        public static string? Host { get; internal set; }
        public static List<string> AllowedIPs { get; internal set; }= new List<string>();
        public static string? AccessKey { get; internal set; }
        public static string? SecretKey { get; internal set; }
        public static string? LoginUrl { get; internal set; }
        public static string? AccesDeniedUrl { get; internal set; }
    }
}
