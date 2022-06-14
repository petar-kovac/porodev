using PoroDev.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace PoroDev.GatewayAPI.Helpers.GlobalExceptionHandler
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
            catch (Exception exception)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                string HumanReadableErrorMessage;
                switch (exception)
                {
                    case UserNotFoundException userNotFound:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        HumanReadableErrorMessage = userNotFound.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, userNotFound);
                        break;

                    case KeyNotFoundException keyNotFound:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        HumanReadableErrorMessage = "Key not found exception";
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, keyNotFound);
                        break;

                    case FailedToLogInException failedToLogIn:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        HumanReadableErrorMessage = failedToLogIn.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, failedToLogIn);
                        break;

                    case EmailFormatException emailFormatException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = emailFormatException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, emailFormatException);
                        break;

                    case PasswordFormatException passwordFormatException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = passwordFormatException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, passwordFormatException);
                        break;

                    case UserExistsException userExistsException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = userExistsException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, userExistsException);
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = "Internal server error";
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, exception);
                        break;
                }

                var result = JsonSerializer.Serialize(new { humanReadableErrorMessage = HumanReadableErrorMessage, exceptionMessage = exception?.Message });
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