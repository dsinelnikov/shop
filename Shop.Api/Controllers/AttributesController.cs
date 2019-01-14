using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributesService _attributesService;

        public AttributesController(IAttributesService attributesService)
        {
            _attributesService = attributesService;
        }

        // GET: api/attributes
        [HttpGet]
        public Task<IEnumerable<AttributeDto>> Get(PaginationDto pagination)
        {
            return _attributesService.GetAttributesAsync(pagination, HttpContext.RequestAborted);
        }

        // GET api/attributes/{attributeId}
        [HttpGet("{attributeId}")]
        public Task<AttributeDto> Get(int attributeId)
        {
            return _attributesService.GetAttributeAsync(attributeId, HttpContext.RequestAborted);
        }

        // POST api/attributeunits
        [HttpPost]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Post([FromBody]AttributeDto attribute)
        {
            return _attributesService.AddAttributeAsync(attribute, HttpContext.RequestAborted);
        }

        // PUT api/attributes/attributeId
        [HttpPut("{attributeId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Put(int attributeId, [FromBody]AttributeDto attribute)
        {
            return _attributesService.UpdateAttributeAsync(attributeId, attribute, HttpContext.RequestAborted);
        }

        // DELETE api/attributes/{attributeId}
        [HttpDelete("{attributeId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Delete(int attributeId)
        {
            return _attributesService.DeleteAttributeAsync(attributeId, HttpContext.RequestAborted);
        }
    }
}