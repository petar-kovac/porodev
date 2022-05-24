using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Access.Layer.Helpers.GlobalExceptionHandler
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                string HRMessage;
                switch (error)
                {
                    case Business.Access.Layer.Helpers.GlobalExceptionHandler.AppException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HRMessage = e.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HRMessage, e);
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        HRMessage = "Key not found exception";
                        ExceptionLogger.WriteNewLog(HRMessage, e);
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HRMessage = "Internal server error";
                        ExceptionLogger.WriteNewLog(HRMessage, new Exception());
                        break;
                }

                var result = JsonSerializer.Serialize(new { humanReadableMessage = HRMessage, exceptionMessage = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
