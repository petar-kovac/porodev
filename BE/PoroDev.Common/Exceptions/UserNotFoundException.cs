﻿using PoroDev.Common.Exceptions.Contract;
using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class UserNotFoundException : Exception, ICustomException
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