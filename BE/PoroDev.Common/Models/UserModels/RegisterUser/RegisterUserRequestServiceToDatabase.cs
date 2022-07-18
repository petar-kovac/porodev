using PoroDev.Common.Enums;

namespace PoroDev.Common.Models.UserModels.RegisterUser
{
    public class RegisterUserRequestServiceToDatabase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public UserEnums.UserRole Role { get; set; }

        public UserEnums.UserDepartment Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime DateCreated { get; set; }

        public ulong FileDownloadTotal { get; set; }

        public ulong FileUploadTotal { get; set; }

        public ushort RuntimeTotal { get; set; }
    }
}