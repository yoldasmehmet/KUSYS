using System;
using System.Collections.Generic;

namespace Library.Common.Entities
{
    public partial class Application
    {
        public Application()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Ip { get; set; }
        public string? Port { get; set; }
        public string? Description { get; set; }
        public int? TicketExpirationSecond { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
