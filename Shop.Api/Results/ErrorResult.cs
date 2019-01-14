using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shop.Api.Results
{
    public class ErrorResult : IActionResult
    {
        private readonly string _errorMessage;
        private readonly HttpStatusCode _statusCode;
        private readonly IDictionary<string, string> _errors;

        public ErrorResult(string errorMessage, HttpStatusCode statusCode)
            : this(errorMessage, statusCode, null)
        {

        }

        public ErrorResult(string errorMessage, HttpStatusCode statusCode, IDictionary<string, string> errors)
        {
            _errorMessage = errorMessage;
            _statusCode = statusCode;
            _errors = errors;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return ExecuteResultAsync(context.HttpContext);
        }

        public Task ExecuteResultAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)_statusCode;

            var result = JsonConvert.SerializeObject(new ExceptionResponse(_errorMessage, _errors),
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            return httpContext.Response.WriteAsync(result);
        }
    }
}
