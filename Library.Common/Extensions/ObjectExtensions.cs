using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public static class ObjectExtensions
{
    /// <summary>
    /// Objeyi json'a dönüştüren metod
    /// </summary>
    /// <param name="o"></param>
    /// <param name="WriteIndented">Girintili olsun mu? değeri</param>
    /// <returns></returns>
    public static string ToJson(this object o,bool WriteIndented=false)
    {
        var settings = new JsonSerializerSettings
        {
            //ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = WriteIndented? Formatting.Indented:Formatting.None
        };
        return JsonConvert.SerializeObject(o, settings);
    }

}

