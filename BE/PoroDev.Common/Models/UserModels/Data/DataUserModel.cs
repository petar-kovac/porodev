using PoroDev.Common.Enums;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.StorageModels.Data;
using System.Text.Json.Serialization;

namespace PoroDev.Common.Models.UserModels.Data
{
    public class DataUserModel
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

        public ulong FileDownloadTotal { get ; set; }

        public ulong FileUploadTotal { get; set; }

        public ushort RuntimeTotal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DateTime DateCreated { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<FileData> fileDatas { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<RuntimeData> runtimeDatas { get; set; }

        public string VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public DataUserModel()
        {

        }

        public DataUserModel(Guid id, string name, string lastname, string email, byte[] password, UserEnums.UserRole role,
                            UserEnums.UserDepartment department, string position, string avatarUrl, DateTime dateCreated, byte[] salt,
                            string verificationToken, DateTime? verifiedAt)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Email = email;
            Password = password;
            Role = role;
            Department = department;
            Position = position;
            AvatarUrl = avatarUrl;
            DateCreated = dateCreated;
            Salt = salt;
            VerificationToken = verificationToken;
            VerifiedAt = verifiedAt;
        }
    }
}