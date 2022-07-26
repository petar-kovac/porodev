using System.Text.Json.Serialization;

namespace PoroDev.Common.Contracts.SharedSpace.QueryFiles
{
    public class QueryFilesResponse
    {
        public string FileId { get; set; }

        public string FileName { get; set; }

        public Guid OwnerId { get; set; }

        public string UserName { get; set; }

        public string UserLastName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? AddedToSharedSpace { get; set; }

        public QueryFilesResponse()
        {
        }

        public QueryFilesResponse(string fileId,
                                  string fileName,
                                  Guid ownerId,
                                  string userName,
                                  string userLastName,
                                  DateTimeOffset? addedToSharedSpace = null)
        {
            FileId = fileId;
            FileName = fileName;
            OwnerId = ownerId;
            UserName = userName;
            UserLastName = userLastName;
            AddedToSharedSpace = addedToSharedSpace ?? null;
        }
    }
}