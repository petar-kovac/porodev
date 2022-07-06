using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class FileUploadException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public FileUploadException() : base()
        {
            HumanReadableErrorMessage = "File failed to upload";
        }

        public FileUploadException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FileUploadException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}