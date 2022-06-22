using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class FileUploadException : Exception
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
