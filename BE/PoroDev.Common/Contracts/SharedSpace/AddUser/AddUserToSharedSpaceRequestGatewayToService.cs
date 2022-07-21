using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace.AddUser
{
    public class AddUserToSharedSpaceRequestGatewayToService
    {
        public Guid SharedSpaceID { get; set; }
        public Guid UserToAddId { get; set; }
    }
}
