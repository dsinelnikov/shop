using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto.Products.Authentication
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
        public int PersonId { get; set; }
    }
}
