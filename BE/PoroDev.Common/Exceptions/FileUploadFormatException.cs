using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class FileUploadFormatException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public FileUploadFormatException() : base()
        {
            HumanReadableErrorMessage = "Invalid format for file update!";
        }

        public FileUploadFormatException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FileUploadFormatException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}