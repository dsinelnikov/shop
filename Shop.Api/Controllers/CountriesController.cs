using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;

namespace Shop.Api.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        // GET: api/brands
        [HttpGet]
        public Task<IEnumerable<CountryDto>> Get()
        {
            return _countriesService.GetCountrisAsync(HttpContext.RequestAborted);
        }

        // GET api/countries/{countryId}
        [HttpGet("{countryId}")]
        public Task<CountryDto> Get(int countryId)
        {
            return _countriesService.GetCountryAsync(countryId, HttpContext.RequestAborted);
        }

        // POST api/countries
        [HttpPost]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Post([FromBody]CountryDto country)
        {
            return _countriesService.AddCountryAsync(country, HttpContext.RequestAborted);
        }

        // PUT api/countries/{countryId}
        [HttpPut("{countryId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Put(int countryId, [FromBody]CountryDto country)
        {
            return _countriesService.UpdateCountryAsync(countryId, country, HttpContext.RequestAborted);
        }

        // DELETE api/countries/{countryId}
        [HttpDelete("{countryId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Delete(int countryId)
        {
            return _countriesService.DeleteCountryAsync(countryId, HttpContext.RequestAborted);
        }
    }
}