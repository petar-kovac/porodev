using PoroDev.Common.Exceptions.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class MonthLimitException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public MonthLimitException() : base()
        {
            HumanReadableErrorMessage = "Month limit is 6 months!";
        }

        public MonthLimitException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public MonthLimitException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
