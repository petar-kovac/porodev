using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class FileUploadFormatException : Exception
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
