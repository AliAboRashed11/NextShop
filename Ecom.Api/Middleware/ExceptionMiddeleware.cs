using Ecom.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

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
    }
}
