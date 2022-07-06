using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class DockerRuntimeException : Exception, ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }

        public DockerRuntimeException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public DockerRuntimeException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public DockerRuntimeException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}