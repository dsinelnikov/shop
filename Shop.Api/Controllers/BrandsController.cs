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
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsService _brandsService;

        public BrandsController(IBrandsService brandsService)
        {
            _brandsService = brandsService;
        }

        // GET: api/brands
        [HttpGet]
        public Task<IEnumerable<BrandDto>> Get()
        {
            return _brandsService.GetBrandsAsync(HttpContext.RequestAborted);
        }

        // GET api/produsts/{brandId}
        [HttpGet("{brandId}")]
        public Task<EditBrandDto> Get(int brandId)
        {
            return _brandsService.GetBrandAsync(brandId, HttpContext.RequestAborted);
        }

        // POST api/brands
        [HttpPost]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Post([FromBody]EditBrandDto brand)
        {
            return _brandsService.AddBrandAsync(brand, HttpContext.RequestAborted);
        }

        // PUT api/brands/{brandId}
        [HttpPut("{brandId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Put(int brandId, [FromBody]EditBrandDto brand)
        {
            return _brandsService.UpdateBrandAsync(brandId, brand, HttpContext.RequestAborted);
        }

        // DELETE api/produsts/{brandId}
        [HttpDelete("{brandId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Delete(int brandId)
        {
            return _brandsService.DeleteBrandAsync(brandId, HttpContext.RequestAborted);
        }
    }
}