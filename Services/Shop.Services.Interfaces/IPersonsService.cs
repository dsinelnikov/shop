using Shop.Services.Dto.Products;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface IPersonsService
    {
        Task<PersonDto> GetPersonAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
