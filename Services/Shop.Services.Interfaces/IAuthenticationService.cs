using Shop.Services.Dto.Products.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserDto> GetUser(string email, string password, CancellationToken cancellationToken = default(CancellationToken));
        Task AddUser(string email, string password, CancellationToken cancellationToken = default(CancellationToken));
    }
}
