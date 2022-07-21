using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace.QueryFiles
{
    public class QueryFilesResponse
    {
        public string FileId { get; set; }

        public string FileName { get; set; }

        public Guid OwnerId { get; set; }

        public string UserName { get; set; }

        public string UserLastName { get; set; }

        public QueryFilesResponse()
        {

        }

        public QueryFilesResponse(string fileId,
                                  string fileName,
                                  Guid ownerId,
                                  string userName,
                                  string userLastName)
        {
            FileId = fileId;
            FileName = fileName;
            OwnerId = ownerId;
            UserName = userName;
            UserLastName = userLastName;
        }
    }
}
