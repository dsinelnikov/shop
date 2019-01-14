using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Api.Results
{
    public class BadRequestResult : ErrorResult
    {
        private const string DefaultErrorMessage = "Some properties has invalid values.";

        public BadRequestResult(string errorMessage, ModelStateDictionary modelState = null)
            :base(errorMessage, HttpStatusCode.BadRequest, ToDictionary(modelState))
        {

        }

        public BadRequestResult(ModelStateDictionary modelState = null)
            : base(GetGeneralErrorMessage(modelState), HttpStatusCode.BadRequest, ToDictionary(modelState))
        {

        }

        private static string GetGeneralErrorMessage(ModelStateDictionary modelState)
        {
            if(modelState == null)
            {
                return DefaultErrorMessage;
            }

            ModelStateEntry generalEntry;
            if(modelState.TryGetValue(string.Empty, out generalEntry))
            {
                return generalEntry.Errors.FirstOrDefault()?.ErrorMessage ?? DefaultErrorMessage;
            }

            return DefaultErrorMessage;
        }

        private static IDictionary<string, string> ToDictionary(ModelStateDictionary modelState)
        {
            if(modelState == null)
            {
                return new Dictionary<string, string>();
            }

            string ToLower(string key)
            {
                if (string.IsNullOrEmpty(key))
                {
                    return key;
                }

                var str = key.ToCharArray();
                str[0] = char.ToLower(str[0]);
                return new string(str);
            }

            return modelState.Where(err => err.Value.Errors.Any())
                .ToDictionary(err => ToLower(err.Key), err => err.Value.Errors.FirstOrDefault()?.ErrorMessage);
        }
    }
}
