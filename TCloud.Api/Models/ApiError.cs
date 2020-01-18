﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TCloud.Api.Models
{
    public class ApiError
    {
        public string Message { get; set; } = "Whoops! Something went wrong!";
        public string CorrelationId { get; set; }
    }

    public class BadRequestError : ApiError
    {
        public IDictionary<string, IEnumerable<string>> PropertyErrors { get; }

        public BadRequestError(ModelStateDictionary modelState)
        {
            PropertyErrors = modelState
                .Where(kv => kv.Value.Errors.Any())
                .ToDictionary(kv => kv.Key, kv => kv.Value.Errors.Select(e => e.ErrorMessage));
        }
    }
}