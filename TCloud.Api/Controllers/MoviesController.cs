using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCloud.Api.Models;

namespace TCloud.Api.Controllers
{
    [Route("movies")]
    public class MoviesController
    {
        public async Task<ListResponse<Movie>> List()
        {
            return new ListResponse<Movie>();
        }
    }
}