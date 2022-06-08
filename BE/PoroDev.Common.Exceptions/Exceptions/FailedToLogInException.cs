using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions.Exceptions
{
    public class FailedToLogInException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public FailedToLogInException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";

        }

        public FailedToLogInException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FailedToLogInException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
