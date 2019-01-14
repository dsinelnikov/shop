using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;
using Shop.Infrastructure.Data;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using Shop.Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Shop.Infrastructure.Business.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ApplicationDbContext _dbContext;

        public CountriesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCountryAsync(CountryDto country, CancellationToken cancellationToken = default(CancellationToken))
        {
            var c = new Country
            {
                Name = country.Name
            };

            try
            {
                _dbContext.Add(c);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(c, ex);
            }
        }

        public async Task DeleteCountryAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var country = new Country
            {
                Id = id
            };

            _dbContext.Attach(country).State = EntityState.Deleted;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Country", ex);
            }
        }

        public async Task<IEnumerable<CountryDto>> GetCountrisAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.Countries
                .Select(c => new CountryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync(cancellationToken);
        }

        public async Task<CountryDto> GetCountryAsync(int countryId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var country = await _dbContext.Countries
                .Select(b => new CountryDto
                {
                    Id = b.Id,
                    Name = b.Name
                }).FirstOrDefaultAsync(cancellationToken);

            if (country == null)
            {
                throw new ItemNotFoundException(countryId, "Country");
            }

            return country;
        }

        public async Task UpdateCountryAsync(int id, CountryDto country, CancellationToken cancellationToken = default(CancellationToken))
        {
            var c = new Brand
            {
                Id = id,
                Name = country.Name,
            };
            _dbContext.Attach(c).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Country", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(c, ex);
            }
        }
    }
}
