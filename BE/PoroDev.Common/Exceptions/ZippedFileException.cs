using PoroDev.Common.Exceptions.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class ZippedFileException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public ZippedFileException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public ZippedFileException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public ZippedFileException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
