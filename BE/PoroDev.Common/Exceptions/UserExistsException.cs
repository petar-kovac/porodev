using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class UserExistsException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public UserExistsException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public UserExistsException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public UserExistsException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}