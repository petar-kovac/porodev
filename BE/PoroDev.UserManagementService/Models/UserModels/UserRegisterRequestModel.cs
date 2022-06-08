namespace PoroDev.UserManagementService.Models.UserModels
{
    public class UserRegisterRequestModel
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public UserRegisterRequestModel(string name, string lastname, string email, string password, int department, string position, string avatarUrl)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Password = password;
            Department = department;
            Position = position;
            AvatarUrl = avatarUrl;
        }
    }
}