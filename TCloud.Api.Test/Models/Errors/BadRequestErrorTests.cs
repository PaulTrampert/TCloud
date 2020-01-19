using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using TCloud.Api.Models.Errors;

namespace TCloud.Api.Test.Models.Errors
{
    public class BadRequestErrorTests
    {
        [Test]
        public void ConstructorBuildsErrorDictionary()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Prop", "Some Error");
            
            var subject = new BadRequestError(modelState);
            
            Assert.That(subject.PropertyErrors, Contains.Key("Prop"));
            Assert.That(subject.PropertyErrors["Prop"].First(), Is.EqualTo("Some Error"));
        }
    }
}