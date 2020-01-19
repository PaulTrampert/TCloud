using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using TCloud.Api.Models.List;

namespace TCloud.Api.Test.Models.List.ListRequestTests
{
    public class WhenValidating : WithListRequest
    {
        [Test]
        public void ItIsValidWhenSortByIsNull()
        {
            var subject = new ListRequest<TestClass> {SortBy = null};

            Assert.That(subject.Validate(new ValidationContext(subject)), Is.Empty);
        }

        [Test]
        public void ItIsValidWhenSortByIsSortableProp()
        {
            var subject = new ListRequest<SortableTestClass> {SortBy = nameof(SortableTestClass.Prop1)};

            Assert.That(subject.Validate(new ValidationContext(subject)), Is.Empty);
        }

        [Test]
        public void ItIsValidWhenSortByIsDefaultSortProp()
        {
            var subject = new ListRequest<ClassWithDefaultSort>();

            Assert.That(subject.Validate(new ValidationContext(subject)), Is.Empty);
        }

        [Test]
        public void ItIsInvalidWhenSortByIsNonSortable()
        {
            var subject = new ListRequest<ClassWithDefaultSort>{SortBy = nameof(SortableTestClass.Prop2)};

            Assert.That(subject.Validate(new ValidationContext(subject)).Count(), Is.EqualTo(1));
        }
    }
}