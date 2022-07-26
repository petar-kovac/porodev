﻿using PoroDev.Common.Models.UserModels.Data;
using System.Text.Json.Serialization;

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

        public bool Compare(Guid userId, Guid sharedSpaceId)
        {
            if (UserId.Equals(userId) && SharedSpaceId.Equals(sharedSpaceId))
                return true;

            return false;
        }
    }
}