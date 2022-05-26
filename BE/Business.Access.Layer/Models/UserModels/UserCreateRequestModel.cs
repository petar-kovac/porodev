using Data.Access.Layer.Models;

namespace Business.Access.Layer.Models.UserModels
{
    public class UserCreateRequestModel
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public Enums.UserDepartment Department { get; set; }

        public Enums.UserRole Role { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public UserCreateRequestModel()
        {
        }

        public UserCreateRequestModel(string name, string lastname, string email, byte[] password, byte[] salt, Enums.UserDepartment department, Enums.UserRole role, string position, string avatarUrl)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Password = password;
            Salt = salt;
            Department = department;
            Role = role;
            Position = position;
            AvatarUrl = avatarUrl;
        }
    }
}