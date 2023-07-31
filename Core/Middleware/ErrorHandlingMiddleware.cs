using System;
using System.Threading.Tasks;
using Core.Model.ErrorModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (CustomException e)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) e.StatusCode;
                await httpContext.Response.WriteAsync(e.ToString());
                _logger.LogInformation(e, "A known error has occurred");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}