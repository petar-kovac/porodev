using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.EmailSender
{
    public class SendEmailRequest
    {
        public Dictionary<string, string> OtherParametersForEmail { get; set; }

        public string Subject { get; set; }

        public string plainTextContent { get; set; }

        public string htmlTextContent { get; set; }

        public string EmailReceiver { get; set; }
    }
}
