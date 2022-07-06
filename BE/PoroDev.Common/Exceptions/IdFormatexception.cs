using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class IdFormatException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public IdFormatException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public IdFormatException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public IdFormatException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}