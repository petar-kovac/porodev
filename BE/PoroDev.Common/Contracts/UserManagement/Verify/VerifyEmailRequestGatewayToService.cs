using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.UserManagement.Verify
{
    public class VerifyEmailRequestGatewayToService
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
