﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Exceptions
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