using PoroDev.Common.Models.StorageModels.Data;
using System.Text.Json.Serialization;

namespace PoroDev.Common.Models.SharedSpaces
{
    public class SharedSpacesFiles
    {
        public Guid SharedSpaceId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public SharedSpace SharedSpace { get; set; }

        public string FileId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public FileData File { get; set; }

        public DateTimeOffset DateAdded { get; set; }

        public bool Compare(string fileId, Guid sharedSpaceId)
        {
            if (FileId.Equals(fileId) && SharedSpaceId.Equals(sharedSpaceId))
                return true;

            return false;
        }
    }
}