using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class UserPermissionException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public UserPermissionException() : base()
        {
            HumanReadableErrorMessage = "User doesn't have permission for that action.";
        }

        public UserPermissionException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public UserPermissionException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}