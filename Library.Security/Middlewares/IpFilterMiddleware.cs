using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Library.Common.Containers;
using System.Runtime.CompilerServices;

namespace Library.Security.Middlewares
{
    public class IpFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationOptions _applicationOptions;
        public IpFilterMiddleware(RequestDelegate next, IOptions<ApplicationOptions> applicationOptionsAccessor)
        {
            _next = next;
            _applicationOptions = applicationOptionsAccessor.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            string? result = "";
            result = GetClientIPAddress(context);
            var isInwhiteListIPList = ApplicationOptions.Whitelist?
                .Where(a => a == result)
                .Any();
            if (isInwhiteListIPList==false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            await _next.Invoke(context);
        }
        public static string? GetClientIPAddress(HttpContext context)
        {
            string? ip = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
            {
                ip = context.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
            }
            return ip;
        }
    }

    public class ApplicationOptions
    {
        internal static void SetAllowedIPs(List<string> _allowedIps)
        {
            allowedIps = _allowedIps;
        }
        static List<string> allowedIps = new List<string>() { };
        static System.TimeSpan ts = new System.TimeSpan(0, 0, 20);
        static DateTime cacheTime = DateTime.Now;
        public static List<string>? whitelist;
        public static List<string>? Whitelist
        {
            get
            {
                if (whitelist == null || cacheTime.Add(ts) < DateTime.Now)
                {
                    cacheTime = DateTime.Now;
                    whitelist = AppSettings.AllowedIPs;
                }
                return whitelist;
            }
        }
    }

}
