using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class InvalidVerificationTokenException
    {
        public string HumanReadableErrorMessage { get; set; }

        public InvalidVerificationTokenException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public InvalidVerificationTokenException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public InvalidVerificationTokenException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}