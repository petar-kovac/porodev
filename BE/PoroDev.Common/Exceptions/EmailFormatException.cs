﻿using System.Globalization;

namespace PoroDev.Common.Exceptions
{
    public class EmailFormatException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public EmailFormatException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";
        }

        public EmailFormatException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public EmailFormatException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}