using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.UserManagement.ReadAllSharedSpacesForUser
{
    public class ReadAllSharedSpacesResponse
    {
        public Guid SharedSpaceId { get; set; }

        public Guid UserId { get; set; }
        
        public string OwnerName { get; set; }

        public string SharedSpaceName { get; set; }

        public ReadAllSharedSpacesResponse()
        {

        }

        public ReadAllSharedSpacesResponse(Guid sharedSpaceId, Guid userId, string firstName, string lastName, string sharedSpaceName)
        {
            SharedSpaceId = sharedSpaceId;

            UserId = userId;

            OwnerName = $"{firstName} {lastName}";

            SharedSpaceName = sharedSpaceName;
        }
    }
}
