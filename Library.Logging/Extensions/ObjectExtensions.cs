using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gazi.Logging.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object o, bool WriteIndented = false)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = WriteIndented
            };
            return JsonSerializer.Serialize(o, options);
        }

    }

}