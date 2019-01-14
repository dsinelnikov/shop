using Microsoft.AspNetCore.Mvc;
using Shop.Services.Dto.Products.Authentication;
using Shop.Services.Interfaces;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // POST api/produsts
        [HttpPost]
        public Task Post([FromBody] NewUserDto user)
        {
            return _authenticationService.AddUser(user.Email, user.Password);
        }
    }
}