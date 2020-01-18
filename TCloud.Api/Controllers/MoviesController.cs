using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TCloud.Api.Filters;
using TCloud.Api.Models;

namespace TCloud.Api.Controllers
{
    [Route("movies")]
    public class MoviesController
    {
        private readonly IMongoCollection<Movie> movies;
        private readonly ILogger<MoviesController> log;

        public MoviesController(IMongoCollection<Movie> movies, ILogger<MoviesController> log)
        {
            this.movies = movies;
            this.log = log;
        }
        
        [HttpGet]
        public async Task<ListResponse<Movie>> List([FromQuery]ListRequest<Movie> request)
        {
            log.LogInformation("{@Request}", request);

            var filteredMovies = movies.Find(request.FilterDefinition);
            var total = filteredMovies.CountDocumentsAsync();
            var results = filteredMovies
                .Sort(request.SortDefinition)
                .Skip(request.Start)
                .Limit(request.PageSize)
                .ToListAsync();
            
            var response = new ListResponse<Movie>
            {
                Start = request.Start,
                PageSize = request.PageSize,
                Total = await total,
                Results = await results
            };
            
            log.LogInformation("{@Response}", response);
            
            return response;
        }
    }
}