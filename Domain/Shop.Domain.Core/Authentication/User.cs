using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core.Authentication
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        public Person Person { get; set; }
    }
}
