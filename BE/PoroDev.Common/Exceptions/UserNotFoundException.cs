using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public UserNotFoundException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public UserNotFoundException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public UserNotFoundException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
