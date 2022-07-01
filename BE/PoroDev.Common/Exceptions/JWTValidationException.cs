using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class JWTValidationException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public JWTValidationException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public JWTValidationException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public JWTValidationException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
