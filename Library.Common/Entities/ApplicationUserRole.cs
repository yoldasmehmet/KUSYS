using System;
using System.Collections.Generic;

namespace Library.Common.Entities
{
    public partial class ApplicationUserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int ApplicationId { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
