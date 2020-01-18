using System.Collections.Generic;
using CorrelationId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TCloud.Api.Filters;

namespace TCloud.Api.Test.Filters.ValidateModelFilterAttributeTests
{
    public abstract class WithValidateModelFilterAttribute
    {
        public ValidateModelFilterAttribute Subject { get; set; }
        
        public Mock<ILogger<ValidateModelFilterAttribute>> Logger { get; set; }
        
        public Mock<ICorrelationContextAccessor> CorrelationCtx { get; set; }
        
        public ModelStateDictionary ModelState { get; set; }
        
        public ActionExecutingContext ExecutingContext { get; set; }
        public ActionContext ActionContext { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            Logger = new Mock<ILogger<ValidateModelFilterAttribute>>();
            CorrelationCtx = new Mock<ICorrelationContextAccessor>();
            ModelState = new ModelStateDictionary();

            ActionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                ModelState
            );

            ExecutingContext = new ActionExecutingContext(
                ActionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );
            Subject = new ValidateModelFilterAttribute(Logger.Object, CorrelationCtx.Object);
        }
    }
}