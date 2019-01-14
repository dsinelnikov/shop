using Shop.Services.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<EditProductCategoryDto> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<ProductCategoryDto>> GetCategoriesAsync(int? parentCategoryId, CancellationToken cancellationToken = default(CancellationToken));
        Task AddCategoryAsync(int? parentId, EditProductCategoryDto category, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateCategoryAsync(int id, EditProductCategoryDto category, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteCategoryAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
