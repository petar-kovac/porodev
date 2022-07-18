using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class FileNotFoundException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public FileNotFoundException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public FileNotFoundException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FileNotFoundException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}