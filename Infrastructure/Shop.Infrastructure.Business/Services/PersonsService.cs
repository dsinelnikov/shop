using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Data;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using Shop.Services.Interfaces.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Business.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PersonDto> GetPersonAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var person = await _dbContext.Persons.Select(p => new PersonDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName
            }).FirstOrDefaultAsync();

            if(person == null)
            {
                throw new ItemNotFoundException(id, "Person");
            }

            return person;
        }
    }
}
