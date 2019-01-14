using Shop.Services.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface IBrandsService
    {
        Task<EditBrandDto> GetBrandAsync(int brandId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<BrandDto>> GetBrandsAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task AddBrandAsync(EditBrandDto brand, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateBrandAsync(int id, EditBrandDto brand, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteBrandAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
