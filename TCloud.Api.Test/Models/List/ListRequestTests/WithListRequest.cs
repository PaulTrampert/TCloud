using NUnit.Framework;
using TCloud.Api.Models.List;

namespace TCloud.Api.Test.Models.List.ListRequestTests
{
    public class ClassWithDefaultSort
    {
        [Sortable]
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        
        [DefaultSort]
        [Sortable]
        public string Prop3 { get; set; }
    }
    
    public class SortableTestClass
    {
        [Sortable]
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        [Sortable]
        public string Prop3 { get; set; }
    }
    public class TestClass
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        public string Prop3 { get; set; }
    }
    
    public abstract class WithListRequest
    {
        [SetUp]
        public virtual void SetUp()
        {
        }
    }
}