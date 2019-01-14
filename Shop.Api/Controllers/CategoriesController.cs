using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Api.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: api/categories
        [HttpGet]
        public Task<IEnumerable<ProductCategoryDto>> Get()
        {
            return _categoriesService.GetCategoriesAsync(null, HttpContext.RequestAborted);
        }

        // GET: api/categories/{parentCategoryId?}/categories
        [HttpGet("{parentCategoryId}/categories")]
        public Task<IEnumerable<ProductCategoryDto>> Get(int? parentCategoryId)
        {
            return _categoriesService.GetCategoriesAsync(parentCategoryId, HttpContext.RequestAborted);
        }

        // GET api/categories/{categoryId}
        [HttpGet("{categoryId}")]
        public Task<EditProductCategoryDto> Get(int categoryId)
        {
            return _categoriesService.GetCategoryAsync(categoryId, HttpContext.RequestAborted);
        }

        // POST api/categories/{parentCategoryId?}
        [HttpPost("{parentCategoryId?}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Post(int? parentCategoryId, [FromBody]EditProductCategoryDto category)
        {
            return _categoriesService.AddCategoryAsync(parentCategoryId, category, HttpContext.RequestAborted);
        }

        // PUT api/categories/{categoryId}
        [HttpPut("{categoryId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Put(int categoryId, [FromBody]EditProductCategoryDto category)
        {
            return _categoriesService.UpdateCategoryAsync(categoryId, category, HttpContext.RequestAborted);
        }

        // DELETE api/categories/{categoryId}
        [HttpDelete("{categoryId}")]
        [ApiAuthorize(ApiRoles.Admin)]
        public Task Delete(int categoryId)
        {
            return _categoriesService.DeleteCategoryAsync(categoryId, HttpContext.RequestAborted);
        }
    }
}
