using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TCloud.Api.Models.Errors;

namespace TCloud.Api.Test.Filters.ValidateModelFilterAttributeTests
{
    public class WhenModelStateIsInvalid : WithValidateModelFilterAttribute
    {
        public override void SetUp()
        {
            base.SetUp();
            ModelState.AddModelError("", "Something Broke");
        }

        [Test]
        public void ItSetsTheResponseAsBadRequestObject()
        {
            Subject.OnActionExecuting(ExecutingContext);
            
            Assert.That(ExecutingContext.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void TheResponseBodyIsABadRequestError()
        {
            Subject.OnActionExecuting(ExecutingContext);

            var result = ExecutingContext.Result as BadRequestObjectResult;
            
            Assert.That(result.Value, Is.InstanceOf<BadRequestError>());
        }
    }
}