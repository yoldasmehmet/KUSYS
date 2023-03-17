
using Library.Security;
using Library.Security.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIPFilter(this IApplicationBuilder builder, List<string>? AllowedIps=null)
    {
        getConfiguration();
        if (AllowedIps != null)
            AppSettings.AllowedIPs.AddRange(AllowedIps);
        ApplicationOptions.SetAllowedIPs(AppSettings.AllowedIPs);
        return builder.UseMiddleware<IpFilterMiddleware>();
    }
    static void getConfiguration()
    {
        var environment = Libary.Common.Utils.EnvironmentHelper.GetConfiguration();
        SetConfig(environment);
        if (AppSettings.IsRegistered == false)
        {
            AppSettings.SetAsRegistered();
       
            //ApplicationOptions.SetAllowedIPs(AppSettings.AllowedIPs);
        }
    }



    private static void SetConfig(IConfiguration env)
    {
        var allowedIps = env.Get<AppSettings>();
        if (allowedIps == null)
        {
            Console.Write("Lütfen application.json dosyasına aşagıdaki konfigürasyonu giriniz." +
                  Environment.NewLine +
          "\"AllowedIPs\": " + Environment.NewLine +
                  "[ " + Environment.NewLine +
                  " \"testIp1\"," + Environment.NewLine +
                   "\"testIp2\"," + Environment.NewLine +
                   "\"testIp3\"," + Environment.NewLine +
                    "\"::1\"," + Environment.NewLine +
                  "]" + Environment.NewLine
                  );

        }

    }
}

