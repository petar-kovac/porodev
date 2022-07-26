using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace.AddUser
{
    public class AddUserToSharedSpaceRequestServiceToDatabase
    {
        public Guid SharedSpaceId { get; set; }
        public Guid UserId { get; set; }
    }
}
