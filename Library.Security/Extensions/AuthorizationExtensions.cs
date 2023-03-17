using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Library.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Library.Security.Services;
using Library.Security.Interfaces;
using Library.Security.DTOs;
using Library.Security.Models;
using Library.Security.Middlewares;
using Libary.Common.Utils;

public static class AuthorizationExtensions
{
    public static WebApplication UseCookieAuthorization(this WebApplicationBuilder builder)
    {
        var config = EnvironmentHelper.GetConfiguration().GetEnvirementVariables<SecurityConfig>();
        if (config != null)
        {
            SetConsts(config);
        }
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Forbidden/";
        });
        var app = builder.Build();
        var cookiePolicyOptions = new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,

        };
        app.UseCookiePolicy(cookiePolicyOptions);
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapDefaultControllerRoute();
        return app;
    }
    public static WebApplication UseJwtTokenAuthorization(this WebApplicationBuilder builder)
    {
        var config = EnvironmentHelper.GetConfiguration().GetEnvirementVariables<SecurityConfig>();
        if (config != null)
        {
            SetConsts(config);
        }
        builder.Services.AddDistributedMemoryCache();
        var assamblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        builder.Services.AddSession(options =>
        {

            options.IdleTimeout = TimeSpan.FromHours(12);
            options.Cookie.Name = $".{assamblyName}.Session"; 
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        var key = builder.Configuration["Jwt:Key"];

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? ""))
            };
        });

        var app = builder.Build();
        app.UseSession();

        app.Use(async (context, next) =>
         {
             var token = context.Session.GetString("Token");
             if (!string.IsNullOrEmpty(token))
             {
                 context.Request.Headers.Add("Authorization", "Bearer " + token);
             }
             await next();
         });
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
    public static WebApplication UseJwtTokenAuthorizationy(this WebApplicationBuilder builder)
    {
        var config = EnvironmentHelper.GetConfiguration().GetEnvirementVariables<SecurityConfig>();
        if (config != null)
        {
            SetConsts(config);
        }
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        var key = builder.Configuration["Jwt:Key"];
        builder.Services.AddAuthorization();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? ""))
            };
        });

        var app = builder.Build();

        app.UseSession();

        app.Use(async (context, next) =>
        {
            var token = context.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }
            await next();
        });
        return app;
    }
    private static void SetConsts(SecurityConfig config)
    {
        AppSettings.AccessKey = config.AccessKey;
    }

}