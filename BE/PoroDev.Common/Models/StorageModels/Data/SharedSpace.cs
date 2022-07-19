using PoroDev.Common.Models.UserModels.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PoroDev.Common.Models.StorageModels.Data
{
    public class SharedSpace
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DataUserModel DataUserModel { get; set; }

        public Guid OwnerId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<SharedSpacesUsers> SharedSpaceUser { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<SharedSpacesFiles> SharedSpaceFile { get; set; }
    }
}