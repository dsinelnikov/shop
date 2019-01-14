using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET: api/categories/{categoryId}/products
        [Route("api/categories")]
        [HttpGet("{categoryId}/products")]
        public Task<IEnumerable<ProductDto>> Get(int categoryId, PaginationDto pagination)
        {
            return _productsService.GetProductsAsync(categoryId, pagination, HttpContext.RequestAborted);
        }

        // GET api/produsts/{productId}
        [HttpGet("{productId}")]
        public Task<EditProductDto> Get(int productId)
        {
            return _productsService.GetProductAsync(productId, HttpContext.RequestAborted);
        }

        // POST api/produsts
        [HttpPost]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Post([FromBody]EditProductDto product)
        {
            return _productsService.AddProductAsync(product, HttpContext.RequestAborted);
        }

        // PUT api/produsts/{productId}
        [HttpPut("{productId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Put(int productId, [FromBody]EditProductDto product)
        {
            return _productsService.UpdateProductAsync(productId, product, HttpContext.RequestAborted);
        }

        // DELETE api/produsts/{productId}
        [HttpDelete("{productId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Delete(int productId)
        {
            return _productsService.DeleteProductAsync(productId, HttpContext.RequestAborted);
        }
    }
}
