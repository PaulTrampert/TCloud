using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace TCloud.Api.Test.Filters.ValidateModelFilterAttributeTests
{
    public class WhenModelStateIsValid : WithValidateModelFilterAttribute
    {
        [Test]
        public void ItDoesNotSetTheResponse()
        {
            Subject.OnActionExecuting(ExecutingContext);
            
            Assert.That(ExecutingContext.Result, Is.Null);
        }
    }
}