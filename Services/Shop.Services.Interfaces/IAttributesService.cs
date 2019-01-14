using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface IAttributesService
    {
        Task<AttributeUnitDto> GetAttributeUnitAsync(int unitId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<AttributeUnitDto>> GetAttributeUnitsAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task AddAttributeUnitAsync(AttributeUnitDto unit, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAttributeUnitAsync(int unitId, AttributeUnitDto unit, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAttributeUnitAsync(int unitId, CancellationToken cancellationToken = default(CancellationToken));


        Task<AttributeDto> GetAttributeAsync(int unitId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<AttributeDto>> GetAttributesAsync(PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAttributeAsync(AttributeDto attribute, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAttributeAsync(int attributeId, AttributeDto attribute, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAttributeAsync(int attributeId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
