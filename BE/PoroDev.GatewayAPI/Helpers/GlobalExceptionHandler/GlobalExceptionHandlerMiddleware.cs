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
                    case NoHeaderWithJwtException noHeaderWithJwtException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = noHeaderWithJwtException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, noHeaderWithJwtException);
                        break;

                    case JWTValidationException jwtValidationException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        HumanReadableErrorMessage = jwtValidationException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, jwtValidationException);
                        break;

                    case DatabaseException databaseException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = databaseException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, databaseException);
                        break;

                    case InvalidCredentialsExceptions invalidCredentials:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = invalidCredentials.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, invalidCredentials);
                        break;

                    case PasswordFormatException passwordFormatException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = passwordFormatException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, passwordFormatException);
                        break;

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

                    case PoroDev.Common.Exceptions.FileNotFoundException fileNotFound:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = fileNotFound.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, fileNotFound);
                        break;

                    case EmailFormatException emailFormatException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = emailFormatException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, emailFormatException);
                        break;

                    case UserExistsException userExistsException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = userExistsException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, userExistsException);
                        break;

                    case FullNameFormatException fullnameFormatException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = fullnameFormatException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, fullnameFormatException);
                        break;

                    case PositionFormatException positionFormatException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        HumanReadableErrorMessage = positionFormatException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, positionFormatException);
                        break;

                    case DockerRuntimeException dockerRuntimeException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = dockerRuntimeException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, dockerRuntimeException);
                        break;

                    case ZippedFileException zippedFileException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = zippedFileException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, zippedFileException);
                        break;

                    case FileUploadExistException fileUploadExistException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = fileUploadExistException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, fileUploadExistException);
                        break;

                    case FileUploadFormatException fileUploadFormatException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = fileUploadFormatException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, fileUploadFormatException);
                        break;

                    case FileUploadException fileUploadException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        HumanReadableErrorMessage = fileUploadException.HumanReadableErrorMessage;
                        ExceptionLogger.WriteNewLog(HumanReadableErrorMessage, fileUploadException);
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