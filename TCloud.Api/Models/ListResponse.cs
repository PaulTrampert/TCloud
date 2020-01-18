using System.Collections.Generic;

namespace TCloud.Api.Models
{
    public class ListResponse<T>
    {
        public int Start { get; set; }
        
        public int PageSize { get; set; }
        
        public int Total { get; set; }
        
        public IEnumerable<T> Results { get; set; }
    }
}