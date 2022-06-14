﻿using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class PasswordFormatException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public PasswordFormatException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public PasswordFormatException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public PasswordFormatException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}