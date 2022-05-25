using System.Globalization;

namespace Api.Access.Layer.Helpers.GlobalExceptionHandler
{
    public class AppException : Business.Access.Layer.Helpers.GlobalExceptionHandler.AppException
    {
        public AppException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public AppException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public AppException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}