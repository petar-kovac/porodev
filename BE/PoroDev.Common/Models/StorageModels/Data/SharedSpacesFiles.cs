using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.StorageModels.Data
{
    public class SharedSpacesFiles
    {
        public Guid SharedSpaceId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public SharedSpace SharedSpace { get; set; }

        public string FileId { get; set; }
    }
}
