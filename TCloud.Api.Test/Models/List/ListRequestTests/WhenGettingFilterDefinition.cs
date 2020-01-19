using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using TCloud.Api.Models.List;

namespace TCloud.Api.Test.Models.List.ListRequestTests
{
    public class WhenGettingFilterDefinition
    {
        [Test]
        public void ItReturnsEmptyFilterDefinitionWhenQueryIsNull()
        {
            var subject = new ListRequest<TestClass>();
            
            Assert.That(subject.FilterDefinition, Is.EqualTo(new FilterDefinitionBuilder<TestClass>().Empty));
        }
        
        
        [Test]
        public void ItReturnsEmptyFilterDefinitionWhenQueryIsEmpty()
        {
            var subject = new ListRequest<TestClass>{Query = ""};
            
            Assert.That(subject.FilterDefinition, Is.EqualTo(new FilterDefinitionBuilder<TestClass>().Empty));
        }
        
        [Test]
        public void ItReturnsEmptyFilterDefinitionWhenQueryIsWhiteSpace()
        {
            var subject = new ListRequest<TestClass>{Query = "  \t"};
            
            Assert.That(subject.FilterDefinition, Is.EqualTo(new FilterDefinitionBuilder<TestClass>().Empty));
        }
        
        [TestCase("SomeString")]
        [TestCase("This is another string")]
        public void ItReturnsATextFilterDefinitionWhenQueryIsSomething(string query)
        {
            var subject = new ListRequest<TestClass>{Query = query};
            
            Assert.That(subject.FilterDefinition, Is.InstanceOf<BsonDocumentFilterDefinition<TestClass>>());
            var filter = subject.FilterDefinition as BsonDocumentFilterDefinition<TestClass>;
            Assert.That(filter.ToJson(), Is.EqualTo($"{{ \"Document\" : {{ \"$text\" : {{ \"$search\" : \"{query}\" }} }} }}"));
        }
    }
}