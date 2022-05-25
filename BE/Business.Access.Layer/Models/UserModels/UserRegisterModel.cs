namespace Business.Access.Layer.Models.UserModels
{
    public class UserRegisterModel
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public UserRegisterModel(string name, string lastname, string email, string password, int department, string position, string avatarUrl)
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
