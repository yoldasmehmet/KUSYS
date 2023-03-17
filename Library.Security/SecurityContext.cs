using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Library.Security.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Library.Security.DTOs;
using Libary.Common.Utils;
using System.Net.Http;

namespace Library.Security
{
    public class SecurityContext
    {
        public static async Task<string> Login(ControllerBase controller, UserModel userModel)//, Func<UserModel, UserDTO> func)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return "";
            }
            IConfiguration c = EnvironmentHelper.GetConfiguration();
            //var validUser = func(userModel);

            if (userModel != null)
            {
                HttpClient client = new HttpClient();
                var token = await client.PostAsync($"{c["Jwt:Issuer"]?.ToString()}", JsonContent.Create(userModel));
                var generatedToken = token.Content.ReadAsStringAsync().Result;//_tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
                if (generatedToken != null && generatedToken != "Error")
                {
                    controller.HttpContext.Session.SetString("Token", generatedToken);//.Decrypt(c["Jwt:Key"]?.ToString()));
                    //controller.Response.Cookies.Append("auth", generatedToken.Decrypt(c["Jwt:Key"]?.ToString()));
                    return generatedToken;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public static async Task<bool> LoginCookieAuthentitation(Controller controller, UserModel userModel, Func<UserModel, UserDTO> func)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return false;
            }
            IConfiguration c = EnvironmentHelper.GetConfiguration();
            var validUser = func(userModel);
            if (validUser == null)
                return false;
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userModel.UserName),
            //new Claim("FullName", user.FullName),
            new Claim(ClaimTypes.Role, "manager")
        };
            ;
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await controller.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return true;
        }
    }
}
