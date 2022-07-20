using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace
{
    public class AddFileToSharedSpaceServiceToDatabase
    {
        public Guid SharedSpaceId { get; set; }

        public string FileId { get; set; }

        public Guid UserId { get; set; }

        public AddFileToSharedSpaceServiceToDatabase()
        {

        }

        public AddFileToSharedSpaceServiceToDatabase(Guid sharedSpaceId, string fileId, Guid userId)
        {
            SharedSpaceId = sharedSpaceId;
            FileId = fileId;
            UserId = userId;
        }
    }
}
