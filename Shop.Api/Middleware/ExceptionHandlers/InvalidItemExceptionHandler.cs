using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Api.Results;
using Shop.Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Api.Middleware.ExceptionHandlers
{
    public class InvalidItemExceptionHandler : ExceptionHandler<InvalidItemException>
    {
        protected override ErrorResult GetResult(InvalidItemException exception)
        {
            return new BadRequestResult();
        }
    }
}
