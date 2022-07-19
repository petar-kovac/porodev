using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.SharedSpaces
{
    public class SharedSpacesUsers
    {
        public Guid SharedSpaceId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public SharedSpace SharedSpace { get; set; }

        public Guid UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DataUserModel User { get; set; }
    }
}
