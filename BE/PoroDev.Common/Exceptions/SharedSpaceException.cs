using PoroDev.Common.Exceptions.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class SharedSpaceException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public SharedSpaceException() : base()
        {
            HumanReadableErrorMessage = "Exception happened while working with Shared Space";
        }

        public SharedSpaceException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public SharedSpaceException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}