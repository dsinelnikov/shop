using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Filters
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiAuthorizeAttribute()
            :base()
        {

        }

        public ApiAuthorizeAttribute(ApiRoles roles)
        {
            var rolesList = Enum.GetValues(typeof(ApiRoles))
                .OfType<ApiRoles>()
                .Where(r => (roles & r) > 0);

            Roles = string.Join(',', rolesList);
        }
    }

    public enum ApiRoles
    {
        Admin = 0xFF,
        User = 0x01
    }
}
