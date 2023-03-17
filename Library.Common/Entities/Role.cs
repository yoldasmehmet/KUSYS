using System;
using System.Collections.Generic;

namespace Library.Common.Entities
{
    public partial class Role
    {
        public Role()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
