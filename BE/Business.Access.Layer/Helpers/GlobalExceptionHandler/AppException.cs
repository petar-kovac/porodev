using log4net.Config;
using System.Globalization;

[assembly: XmlConfigurator(ConfigFile = "log4net.config")]

namespace Business.Access.Layer.Helpers.GlobalExceptionHandler
{
    public class AppException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

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