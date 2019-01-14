using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Middleware.ExceptionHandlers
{
    public interface IExceptionHandler
    {
        bool CanHandle(Exception ex);
        Task Handle(HttpContext httpContext, Exception ex);
    }
}
