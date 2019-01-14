using Microsoft.AspNetCore.Mvc;
using Shop.Api.Results;
using System.Collections.Generic;
using System.Net;

namespace Shop.Api.Controllers
{
    [Route("Errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private IDictionary<HttpStatusCode, string> _errorMessages = new Dictionary<HttpStatusCode, string>
        {
            { HttpStatusCode.NotFound, "Route is invalid."},
            { HttpStatusCode.InternalServerError, "Internal server error."},
            { HttpStatusCode.Unauthorized, "Unauthorized access."},
            { HttpStatusCode.Forbidden, "Access is forbidden."},
        };

        [HttpGet("{statusCode}")]
        [HttpPost("{statusCode}")]
        [HttpPut("{statusCode}")]
        [HttpDelete("{statusCode}")]
        public IActionResult Get(HttpStatusCode statusCode)
        {
            string errorMessage;
            if(!_errorMessages.TryGetValue(statusCode, out errorMessage))
            {
                errorMessage = "Something wrong.";
            }

            return new ErrorResult(errorMessage, statusCode);
        }
    }
}