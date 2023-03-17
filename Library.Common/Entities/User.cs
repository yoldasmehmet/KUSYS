using System;
using System.Collections.Generic;

namespace Library.Common.Entities
{
    public partial class User
    {
        public User()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? AuthenticationTypeId { get; set; }
        public string? Actor { get; set; }

        public virtual AuthenticationType? AuthenticationType { get; set; }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
