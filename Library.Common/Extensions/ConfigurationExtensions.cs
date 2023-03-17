using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static  class ConfigurationExtensions
    {
        public static T GetEnvirementVariables<T>(this IConfiguration env)
        {
            var minioConfig = env.GetSection("minio").Get<T>();
            if (minioConfig == null)
            {
                string message = "Lütfen application.json dosyasına aşagıdaki konfigürasyonu giriniz." + Environment.NewLine;
                foreach (var item in typeof(T).GetProperties())
                {
                    message += "\"" + item.Name + "\":degeri" + Environment.NewLine;
                }
                Console.Write(message);
            }

            return minioConfig;
        }
    }

