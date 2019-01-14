using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Api.Results;

namespace Shop.Api.Middleware.ExceptionHandlers
{
    public class UnauthorizedAccessExceptionHandler : ExceptionHandler<UnauthorizedAccessException>
    {
        protected override ErrorResult GetResult(UnauthorizedAccessException exception)
        {
            return new ErrorResult(exception.Message, System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
