using PoroDev.Common.Enums;

namespace PoroDev.UserManagementService.Models.UserModels
{
    public class UserRegisterResponseModel
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public UserRegisterResponseModel()
        {
        }

        public UserRegisterResponseModel(string name, string lastname, string email, UserEnums.UserRole role, UserEnums.UserDepartment department, string position, string avatarUrl)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Role = role.ToString();
            Department = department.ToString();
            Position = position;
            AvatarUrl = avatarUrl;
        }
    }
}