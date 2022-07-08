using PoroDev.Common.Exceptions.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class FileUploadExistException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public FileUploadExistException() : base()
        {
            HumanReadableErrorMessage = "File already exists!";
        }

        public FileUploadExistException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FileUploadExistException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
