using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class InvalidCredentialsExceptions : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public InvalidCredentialsExceptions() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public InvalidCredentialsExceptions(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public InvalidCredentialsExceptions(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}