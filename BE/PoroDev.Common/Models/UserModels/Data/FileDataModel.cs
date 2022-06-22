using PoroDev.Common.Enums;
using PoroDev.Common.Models.StorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.UserModels.Data
{
    public class FileDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public byte[] Password { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public byte[] Salt { get; set; }

        public UserEnums.UserRole Role { get; set; }

        public UserEnums.UserDepartment Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DateTime DateCreated { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<FileData> fileDatas { get; set; }
    }
}
