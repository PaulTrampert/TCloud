using System.Collections.Generic;

namespace TCloud.Api.Models.List
{
    public class ListResponse<T>
    {
        public int Start { get; set; }
        
        public int PageSize { get; set; }
        
        public long Total { get; set; }
        
        public IEnumerable<T> Results { get; set; } = new List<T>();
    }
}