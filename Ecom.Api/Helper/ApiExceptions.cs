namespace Ecom.Api.Helper
{
    public class ApiExceptions : ResponseApi
    {
        public ApiExceptions(int statusCode, string? message = null,string detail = null) : base(statusCode, message)
        {
            Detail = detail;
        }
        public string Detail { get; set; }
    }
}
