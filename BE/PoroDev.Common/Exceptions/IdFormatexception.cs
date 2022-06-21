using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class IdFormatexception : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public IdFormatexception() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public IdFormatexception(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public IdFormatexception(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
