using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;
using Shop.Domain.Core.Authentication;
using Shop.Infrastructure.Data;
using Shop.Services.Dto.Products.Authentication;
using Shop.Services.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthenticationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(string email, string password, CancellationToken cancellationToken = default(CancellationToken))
        {           
            var user = new User
            {
                Email = email,
                Password = password
            };
            await _dbContext.AddAsync(user, cancellationToken);

            var person = new Person
            {
                UserId = user.Id
            };
            await _dbContext.AddAsync(person, cancellationToken);

            var userRole = new UserRole
            {
                RoleId = RoleType.User,
                UserId = user.Id
            };
            await _dbContext.AddAsync(userRole, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<UserDto> GetUser(string email, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbContext.Users
                .Where(u => u.Email == email && u.Password == password)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = u.Roles.Select(r => new RoleDto
                    {
                        Id = (int)r.Role.Id,
                        Name = r.Role.Name
                    }).ToList(),
                    PersonId = u.Person.Id
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
