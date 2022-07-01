using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class NoHeaderWithJwtException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public NoHeaderWithJwtException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public NoHeaderWithJwtException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public NoHeaderWithJwtException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}