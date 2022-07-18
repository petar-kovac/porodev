using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class FailedToRegisterUserException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public FailedToRegisterUserException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public FailedToRegisterUserException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FailedToRegisterUserException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}