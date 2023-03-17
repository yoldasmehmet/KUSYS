//using Library.Common.Utils;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Security.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Actor { get; set; }
        public List<string> Roles { get; set; }
        public int? ExpirationSecond { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
    }
}
public enum AuthenticationType
{
  None,
  LDAP=1,
  Custom=2
}
//public interface IUserRepository
//{
//    UserDTO? GetUser(UserModel userMode);
//}