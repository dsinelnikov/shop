using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core.Authentication
{
    public class Role
    {
        public RoleType Id { get; set; }
        public string Name { get; set; }
    }

    public enum RoleType
    {
        Admin = 1,
        User = 2
    }
}
