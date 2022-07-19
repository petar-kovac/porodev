using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class UserNotVerifiedException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public UserNotVerifiedException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public UserNotVerifiedException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public UserNotVerifiedException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
