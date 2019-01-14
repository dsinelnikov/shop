using Shop.Services.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Services.Interfaces
{
    public interface ICountriesService
    {
        Task<CountryDto> GetCountryAsync(int countryId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<CountryDto>> GetCountrisAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task AddCountryAsync(CountryDto country, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateCountryAsync(int id, CountryDto country, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteCountryAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
