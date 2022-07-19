using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class UserAlreadyVerifiedException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public UserAlreadyVerifiedException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public UserAlreadyVerifiedException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public UserAlreadyVerifiedException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}