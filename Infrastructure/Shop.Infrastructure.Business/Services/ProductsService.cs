using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;
using Shop.Infrastructure.Data;
using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using Shop.Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Business.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(EditProductDto product, CancellationToken cancellationToken = default(CancellationToken))
        {
            var p = new Product
            {
                Name = product.Name,
                Price = product.Price,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                ManufactureCountryId = product.ManufactureCountryId
            };

            try
            {
                _dbContext.Add(p);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(p, ex);
            }
        }

        public async Task DeleteProductAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var product = new Product
            {
                Id = id
            };

            _dbContext.Attach(product).State = EntityState.Deleted;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Product", ex);
            }
        }

        public async Task<EditProductDto> GetProductAsync(int productId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var product = await _dbContext.Products
                .Select(p => new EditProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    BrandId = p.BrandId,
                    CategoryId = p.CategoryId,
                    ManufactureCountryId = p.ManufactureCountryId
                }).FirstOrDefaultAsync(cancellationToken);

            if(product == null)
            {
                throw new ItemNotFoundException(productId, "Product");
            }

            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int categoryId, PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.Products
                .Where(p => p.Category.Id == categoryId)
                .Skip(pagination.Offset)
                .Take(pagination.Count)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToListAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(int id, EditProductDto product, CancellationToken cancellationToken = default(CancellationToken))
        {
            var p = new Product
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                ManufactureCountryId = product.ManufactureCountryId
            };
            _dbContext.Attach(p).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Product", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(p, ex);
            }
        }
    }
}
