using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Security.Middlewares
{

    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _nextMiddleWare;
        public AuthorizationMiddleware(RequestDelegate next)
        {
            _nextMiddleWare = next;
        }
        public async Task Invoke(HttpContext context)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string authHeader = context.Request.Headers["Authorization"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            try
            {
                if (!string.IsNullOrEmpty(authHeader) && authHeader.Contains("known_sign_in=", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Split("known_sign_in=")[1].Split(";")[0];
                    var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var credentials = credentialString.Split(':');
                    if (credentials[0] == "hakan" && credentials[1] == "123")
                    {
                        var claims = new[]
                        {
                            new Claim("name", credentials[0]),
                            new Claim(ClaimTypes.Role, "Admin")
                        };
                        var identity = new ClaimsIdentity(claims, "Basic");
                        context.User = new ClaimsPrincipal(identity);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            await _nextMiddleWare(context);
        }
    }

}
