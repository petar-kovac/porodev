using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace.GetAllUsers
{
    public class GetAllUsersFromSharedSpaceRequestServiceToDatabase
    {
        public Guid SharedSpaceId { get; set; }
    }
}
