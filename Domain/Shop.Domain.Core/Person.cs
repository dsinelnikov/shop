using Shop.Domain.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
