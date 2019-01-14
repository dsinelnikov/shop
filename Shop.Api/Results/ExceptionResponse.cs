using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Results
{
    public class ExceptionResponse
    {
        public string ErrorMessage { get; set; }

        public IDictionary<string, string> Errors { get; set; }

        public ExceptionResponse(string errorMessage)
            : this(errorMessage, null)
        {
        }

        public ExceptionResponse(string errorMessage, IDictionary<string, string> errors)
        {
            ErrorMessage = errorMessage;
            Errors = errors;
        }
    }
}
