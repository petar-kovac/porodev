using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class FullNameFormatException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public FullNameFormatException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public FullNameFormatException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public FullNameFormatException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
