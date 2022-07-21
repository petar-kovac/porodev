using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.UserManagement.ReadAllSharedSpacesForUser
{
    public class ReadAllSharedSpacesForUserRequestGatewayToService
    {
        public Guid UserId { get; set; }
    }
}
