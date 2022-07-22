using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class SharedSpaceNameFormatException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public SharedSpaceNameFormatException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public SharedSpaceNameFormatException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public SharedSpaceNameFormatException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}