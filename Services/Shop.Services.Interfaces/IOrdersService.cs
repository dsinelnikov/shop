using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface IOrdersService
    {
        Task AddOrder(NewOrderDto order, CancellationToken cancellationToken = default(CancellationToken));
        Task<OrderDto> GetOrder(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<OrderDto>> GetOrders(PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<OrderDto>> GetPersonOrders(int personId, PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken));
    }
}
