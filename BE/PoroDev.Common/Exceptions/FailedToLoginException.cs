using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class FailedToLogInException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public FailedToLogInException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public FailedToLogInException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FailedToLogInException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}