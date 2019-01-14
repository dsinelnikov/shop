using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Api.Middleware.ExceptionHandlers
{
    public abstract class ExceptionHandler<TException> : IExceptionHandler
        where TException : Exception
    {
        public bool CanHandle(Exception ex)
        {
            return ex is TException;
        }

        public Task Handle(HttpContext httpContext, Exception exception)
        {
            var result = GetResult((TException)exception);

            return result.ExecuteResultAsync(httpContext);
        }

        protected abstract ErrorResult GetResult(TException exception);
    }
}
