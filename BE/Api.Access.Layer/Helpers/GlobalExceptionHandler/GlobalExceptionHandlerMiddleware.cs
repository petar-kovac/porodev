using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using System.Net;
using System.Text.Json;

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

                string HumanReadableErrorMessage;
                switch (error)
                {
                    case Business.Access.Layer.Helpers.GlobalExceptionHandler.AppException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = e.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, e);
                        break;

                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        HumanReadableErrorMessage = "Key not found exception";
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, e);
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = "Internal server error";
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, new Exception());
                        break;
                }

                var result = JsonSerializer.Serialize(new { humanReadableErrorMessage = HumanReadableErrorMessage, exceptionMessage = error?.Message });
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