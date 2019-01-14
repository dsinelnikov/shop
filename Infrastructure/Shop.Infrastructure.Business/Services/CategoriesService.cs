using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;
using Shop.Infrastructure.Data;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using Shop.Services.Interfaces.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Business.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoriesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategoryAsync(int? parentId, EditProductCategoryDto category, CancellationToken cancellationToken = default(CancellationToken))
        {
            var c = new ProductCategory
            {
                Name = category.Name,
                ParentId = parentId
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

        public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var category = new ProductCategory
            {
                Id = id
            };

            try
            {
                _dbContext.Attach(category).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Category", ex);
            }
        }

        public async Task<EditProductCategoryDto> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var category = await _dbContext.ProductCategories
                .Select(c => new EditProductCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                throw new ItemNotFoundException(categoryId, "Category");
            }

            return category;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetCategoriesAsync(int? parentCategoryId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.ProductCategories
                .Where(c => c.ParentId == parentCategoryId)
                .Select(c => new ProductCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync(cancellationToken);
        }

        public async Task UpdateCategoryAsync(int id, EditProductCategoryDto category, CancellationToken cancellationToken = default(CancellationToken))
        {
            var c = new ProductCategory
            {
                Id = id,
                Name = category.Name
            };
            _dbContext.Attach(c).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(id, "Category", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(c, ex);
            }
        }
    }
}
