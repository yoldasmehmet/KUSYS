using System.Text;
using System.Security.Claims;
//using Library.Common.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Library.Security.Interfaces;
using Library.Security.DTOs;
using System;
using System.Collections.Generic;

namespace Library.Security.Services
{
    public class TokenService : ITokenService
    {
        private int EXPIRY_DURATION_MINUTES = 10;

        public string BuildToken(string key, string issuer, string audience, UserDTO user, int ticketEpirationSexond)
        {
            var claims =new  List<Claim> {
            new Claim(ClaimTypes.Name, user.UserName??"-"),
              new Claim(ClaimTypes.Actor, user.Actor??"-"),
            new Claim(ClaimTypes.NameIdentifier,
            Guid.NewGuid().ToString())
        };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role ?? "-"));
            }
            EXPIRY_DURATION_MINUTES = ticketEpirationSexond;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public bool ValidateToken(string key, string issuer, string audience, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}