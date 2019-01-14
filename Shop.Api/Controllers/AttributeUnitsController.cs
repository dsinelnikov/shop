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
    [Route("api/attributeunits")]
    [ApiController]
    public class AttributeUnitsController : ControllerBase
    {
        private readonly IAttributesService _attributesService;

        public AttributeUnitsController(IAttributesService attributesService)
        {
            _attributesService = attributesService;
        }

        // GET: api/attributeunits
        [HttpGet]
        public Task<IEnumerable<AttributeUnitDto>> Get()
        {
            return _attributesService.GetAttributeUnitsAsync(HttpContext.RequestAborted);
        }

        // GET api/attributeunits/{attributeUnitId}
        [HttpGet("{attributeUnitId}")]
        public Task<AttributeUnitDto> Get(int attributeUnitId)
        {
            return _attributesService.GetAttributeUnitAsync(attributeUnitId, HttpContext.RequestAborted);
        }

        // POST api/attributeunits
        [HttpPost]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Post([FromBody]AttributeUnitDto attributeUnit)
        {
            return _attributesService.AddAttributeUnitAsync(attributeUnit, HttpContext.RequestAborted);
        }

        // PUT api/attributeunits/attributeUnitId
        [HttpPut("{attributeUnitId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Put(int attributeUnitId, [FromBody]AttributeUnitDto attributeUnit)
        {
            return _attributesService.UpdateAttributeUnitAsync(attributeUnitId, attributeUnit, HttpContext.RequestAborted);
        }

        // DELETE api/attributeunits/{attributeUnitId}
        [HttpDelete("{attributeUnitId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Delete(int attributeUnitId)
        {
            return _attributesService.DeleteAttributeUnitAsync(attributeUnitId, HttpContext.RequestAborted);
        }
    }
}