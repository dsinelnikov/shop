using Shop.Infrastructure.Data;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Shop.Services.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;

namespace Shop.Infrastructure.Business.Services
{
    public class BrandsService : IBrandsService
    {
        private readonly ApplicationDbContext _dbContext;

        public BrandsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBrandAsync(EditBrandDto brand, CancellationToken cancellationToken = default(CancellationToken))
        {
            var b = new Brand
            {
                Name = brand.Name,
                RegistrationCountryId = brand.RegistrationCountryId
            };

            try
            {
                _dbContext.Add(b);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(b, ex);
            }
        }

        public async Task DeleteBrandAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var brand = new Brand
            {
                Id = id
            };

            _dbContext.Attach(brand).State = EntityState.Deleted;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Brand", ex);
            }
        }

        public async Task<EditBrandDto> GetBrandAsync(int brandId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var brand = await _dbContext.Brands
                .Select(b => new EditBrandDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    RegistrationCountryId = b.RegistrationCountry.Id
                }).FirstOrDefaultAsync(cancellationToken);

            if (brand == null)
            {
                throw new ItemNotFoundException(brandId, "Brand");
            }

            return brand;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.Brands
                .Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name,                    
                }).ToListAsync(cancellationToken);
        }

        public async Task UpdateBrandAsync(int id, EditBrandDto brand, CancellationToken cancellationToken = default(CancellationToken))
        {
            var b = new Brand
            {
                Id = id,
                Name = brand.Name,
                RegistrationCountryId = brand.RegistrationCountryId
            };
            _dbContext.Attach(b).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Brand", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(b, ex);
            }
        }
    }
}
