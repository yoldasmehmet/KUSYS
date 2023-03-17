using Microsoft.AspNetCore.Http.Features;
namespace Libary.Common.Utils
{

    public  class NetworkUtils
    {
        /// <summary>
        /// HttpContext içerinde istekte bulunan client ip'sini alan metod.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIPAddress(Microsoft.AspNetCore.Http.HttpContext context)
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
            {
                ip = context.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
