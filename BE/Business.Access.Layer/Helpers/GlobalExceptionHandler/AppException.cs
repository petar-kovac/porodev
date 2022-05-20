using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: XmlConfigurator(ConfigFile = "log4net.config")]

namespace Business.Access.Layer.Helpers.GlobalExceptionHandler
{
    public class AppException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }
        public AppException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
            var log = LogManager.GetLogger("Default");
            log.Debug(HumanReadableErrorMessage, this);
        }

        public AppException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }
        public AppException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
