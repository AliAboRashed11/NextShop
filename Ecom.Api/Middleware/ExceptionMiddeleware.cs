using Ecom.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom.Api.Middleware
{
    public class ExceptionMiddeleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _reteLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddeleware(RequestDelegate next,
            IHostEnvironment environment,
            IMemoryCache memoryCache
             )
        {
            _next = next;
            _environment = environment;
            _memoryCache = memoryCache;
        }
            
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                 ApplySecurity(context);

                if (IsRequsetAllowed(context)== false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = 
                    new ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");
                    await context.Response.WriteAsJsonAsync(response);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
                 context.Response.ContentType = "application/json";
                var response =_environment.IsDevelopment() 
                    ? new ApiExceptions((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace):
                    new ApiExceptions((int)HttpStatusCode.InternalServerError,ex.Message);
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }


        // Simple rate limiting: max 8 requests per 30 seconds per IP
        private bool IsRequsetAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cachKey = $"Rate-{ip}";
            var dateNow = DateTime.Now;
            var (timeStamp, count) = _memoryCache.GetOrCreate(cachKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _reteLimitWindow;
                return (_reteLimitWindow: dateNow, count: 0);
            });
            if (dateNow - timeStamp < _reteLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cachKey, (timeStamp, count += 1),_reteLimitWindow);
            }
            else
            {
                _memoryCache.Set(cachKey, (timeStamp, count ), _reteLimitWindow);
            }
            return true;
        }

        private void  ApplySecurity(HttpContext context)
        {
            // X-Content-Type-Options: nosniff
            // Prevents the browser from guessing the file type, stick to the provided MIME type
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            // X-XSS-Options: 1;mode=block
            // Protects against XSS attacks, if malicious code is detected, it blocks execution
            context.Response.Headers["X-XSS-Options"] = "1;mode=block";
            // X-Frame-Options: DENY
            // Prevents the site from being displayed in an iframe on other websites to avoid Clickjacking
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
