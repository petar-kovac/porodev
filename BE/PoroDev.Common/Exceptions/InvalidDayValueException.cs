using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class InvalidDayValueException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public InvalidDayValueException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public InvalidDayValueException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public InvalidDayValueException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}