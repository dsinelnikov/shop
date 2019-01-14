
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
    public class ItemNotFoundExceptionHandler : ExceptionHandler<ItemNotFoundException>
    {
        protected override ErrorResult GetResult(ItemNotFoundException exception)
        {
            return new ErrorResult(exception.Message, HttpStatusCode.NotFound);
        }
    }
}
