using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shop.Api.Middleware.ExceptionHandlers;

namespace Shop.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<IExceptionHandler> _handlers;

        public ExceptionHandlingMiddleware(RequestDelegate next, IEnumerable<IExceptionHandler> handlers)
        {
            _next = next;
            _handlers = handlers.ToList();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var handler = _handlers.FirstOrDefault(h => h.CanHandle(ex));

                if(handler != null)
                {
                    await handler.Handle(httpContext, ex);
                }
                else
                {
                    throw;
                }                
            }            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>((IEnumerable<IExceptionHandler>)new IExceptionHandler[] {
                new ItemNotFoundExceptionHandler(),
                new InvalidItemExceptionHandler()
            });
        }
    }
}
