using System;
using System.Collections.Generic;

namespace Library.Common.Entities
{
    public partial class AuthenticationType
    {
        public AuthenticationType()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
