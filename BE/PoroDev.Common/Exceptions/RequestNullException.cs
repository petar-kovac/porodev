using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class RequestNullException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public RequestNullException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public RequestNullException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public RequestNullException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}