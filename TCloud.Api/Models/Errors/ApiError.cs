namespace TCloud.Api.Models.Errors
{
    public class ApiError
    {
        public string Message { get; set; } = "Whoops! Something went wrong!";
        public string CorrelationId { get; set; }
    }
}