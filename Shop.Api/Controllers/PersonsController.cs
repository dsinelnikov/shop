using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }

        // GET: api/persons/{personId}
        [HttpGet("{personId}")]
        [ApiAuthorize(ApiRoles.User)]
        public Task<PersonDto> Get(int personId)
        {
            return _personsService.GetPersonAsync(personId, HttpContext.RequestAborted);
        }
    }
}