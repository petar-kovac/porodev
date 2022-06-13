using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class DatabaseException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public DatabaseException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public DatabaseException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public DatabaseException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}