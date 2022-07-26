using PoroDev.Common.Exceptions.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class InvalidHourValueException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public InvalidHourValueException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public InvalidHourValueException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public InvalidHourValueException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
