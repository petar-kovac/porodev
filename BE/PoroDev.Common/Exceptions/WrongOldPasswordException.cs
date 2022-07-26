using PoroDev.Common.Exceptions.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class WrongOldPasswordException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public WrongOldPasswordException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public WrongOldPasswordException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public WrongOldPasswordException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
