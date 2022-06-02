﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Exceptions
{
    public class PositionFormatException : Exception
    {
        public string HumanReadableErrorMessage { get; set; }

        public PositionFormatException() : base()
        {
            HumanReadableErrorMessage = "Internal server error occurred";

        }

        public PositionFormatException(string message) : base()
        {
            HumanReadableErrorMessage = message;
        }

        public PositionFormatException(string message, params object[] args) : base()
        {
            HumanReadableErrorMessage = string.Format(CultureInfo.CurrentCulture, message, args);
        }
    }
}
