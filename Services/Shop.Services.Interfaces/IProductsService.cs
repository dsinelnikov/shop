using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface IProductsService
    {
        Task<EditProductDto> GetProductAsync(int productId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<ProductDto>> GetProductsAsync(int categoryId, PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken));
        Task AddProductAsync(EditProductDto product, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateProductAsync(int id, EditProductDto product, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteProductAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
