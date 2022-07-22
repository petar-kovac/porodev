using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace.AddFile
{
    public class AddFileToSharedSpaceGatewayToService
    {
        public Guid SharedSpaceId { get; set; }

        public string FileId { get; set; }

        public Guid UserId { get; set; }

        public AddFileToSharedSpaceGatewayToService()
        {

        }

        public AddFileToSharedSpaceGatewayToService(Guid sharedSpaceId, string fileId, Guid userId)
        {
            SharedSpaceId = sharedSpaceId;
            FileId = fileId;
            UserId = userId;
        }
    }
}
