using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Library.Security.Models;
using Library.Security.Interfaces;
using Library.Security.DTOs;
using Library.Common.Entities;
using System.Text.Json;

namespace Library.Security.IdentityServer.Controllers
{
    [ApiController()]
    [Route("[controller]")]
    public class TokenBuilderController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly ServiceAuthorizationDBContext _serviceAuthorizationDBContext;
        private string generatedToken = null;
        private readonly ILogger<TokenBuilderController> _logger;
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public string Login(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return "Error";
            }
            IActionResult response = Unauthorized();
            var validUser = GetUser(userModel);

            if (validUser != null)
            {
                var key = _config["Jwt:Key"].ToString();
                var exprationSecond = 10;
                if (validUser.ExpirationSecond != null)
                {
                    exprationSecond = validUser.ExpirationSecond.Value;
                }
                generatedToken = _tokenService.BuildToken(key, _config["Jwt:Issuer"].ToString(), _config["Jwt:Audience"]?.ToString(), validUser, exprationSecond);
                if (generatedToken != null)
                {
                    //HttpContext.Session.SetString("Token", generatedToken);
                    var encryptedToken = generatedToken;//.Encrypt(key);
                    return encryptedToken;
                }
                else
                {
                    return "Error";
                }
            }
            else
            {
                return "Error";
            }
        }
        private UserDTO? GetUser(UserModel userModel)
        {
            var user = _serviceAuthorizationDBContext.Users.Where(x => x.ApplicationUserRoles.Any(a => a.Application.Name == userModel.ApplicationName) &&
            x.Name == userModel.UserName && (x.Password == userModel.Password || x.AuthenticationTypeId == (int)AuthenticationType.LDAP)).
            Select(x => new UserDTO
            {
                AuthenticationType = (AuthenticationType)(x.AuthenticationTypeId ?? 0),
                ExpirationSecond = x.ApplicationUserRoles.First(a => a.Application.Name == userModel.ApplicationName).Application.TicketExpirationSecond,
                UserName = x.Name,
                Password = x.Password,
                Actor=x.Actor,
                Roles = x.ApplicationUserRoles.Select(x => x.Role.Name).ToList()
            }).FirstOrDefault();
            if(user!=null&&user.AuthenticationType==AuthenticationType.LDAP)
            {
                return LdapAuth(userModel);
            }
            return user;
        }
        UserDTO LdapAuth(UserModel model)
        {
            return new UserDTO();
        }
        public TokenBuilderController(ILogger<TokenBuilderController> logger, IConfiguration config, ITokenService tokenService, ServiceAuthorizationDBContext serviceAuthorizationDBContext)
        {
            _config = config;
            _logger = logger;
            _tokenService = tokenService;
            _serviceAuthorizationDBContext = serviceAuthorizationDBContext;
    
        }

    }

}