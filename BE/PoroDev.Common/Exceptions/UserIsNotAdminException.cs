using PoroDev.Common.Exceptions.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions
{
    public class UserIsNotAdminException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public UserIsNotAdminException() : base()
        {
            HumanReadableErrorMessage = "User must be admin!";
        }

        public UserIsNotAdminException(string humanReadableErrorMessage) : base()
        {
            HumanReadableErrorMessage = humanReadableErrorMessage;
        }

        public UserIsNotAdminException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }

    }
}
