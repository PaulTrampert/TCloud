using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TCloud.Api.Models.Errors;

namespace TCloud.Api.Filters
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger<ValidateModelFilterAttribute> log;
        private readonly ICorrelationContextAccessor correlationCtx;

        public ValidateModelFilterAttribute(ILogger<ValidateModelFilterAttribute> log, ICorrelationContextAccessor correlationCtx)
        {
            this.log = log;
            this.correlationCtx = correlationCtx;
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            log.LogWarning("Bad request: {@ModelState}", context.ModelState);
            var response = new BadRequestError(context.ModelState)
            {
                CorrelationId = correlationCtx?.CorrelationContext?.CorrelationId
            };
            context.Result = new BadRequestObjectResult(response);
        }
    }
}