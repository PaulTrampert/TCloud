using System;
using System.Collections.Generic;
using TCloud.Api.Models.List;

namespace TCloud.Api.Models
{
    public class Movie
    {
        public string Id { get; set; }
        
        [DefaultSort]
        [Sortable]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public IEnumerable<string> Directors { get; set; }
        
        public IEnumerable<string> Writers { get; set; }
        
        public IEnumerable<string> Producers { get; set; }
        
        [Sortable]
        public string Studio { get; set; }
        
        [Sortable]
        public DateTimeOffset ReleaseDate { get; set; }
        
        public IEnumerable<string> Cast { get; set; }
        
        public string BoxArtUrl { get; set; }
        
        public string BackdropUrl { get; set; }
        
        public string PlaybackUrl { get; set; }
    }
}