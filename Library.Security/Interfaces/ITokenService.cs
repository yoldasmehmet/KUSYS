//using Library.Common.Utils;

using Library.Security.DTOs;

namespace Library.Security.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, string audience, UserDTO user, int ticketEpirationSexond = 10);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}

//public interface IUserRepository
//{
//    UserDTO? GetUser(UserModel userMode);
//}