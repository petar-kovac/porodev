using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class UserLimitException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public UserLimitException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public UserLimitException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public UserLimitException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
