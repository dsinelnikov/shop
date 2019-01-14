using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core.Authentication
{
    public class UserRole
    {
        public int UserId { get; set; }
        public RoleType RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
