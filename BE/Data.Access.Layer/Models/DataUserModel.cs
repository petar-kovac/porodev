using Data.Access.Layer.Models.Contracts;

namespace Data.Access.Layer.Models
{
    public class DataUserModel : IUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public Enums.UserRole Role { get; set; }

        public Enums.UserDepartment Department { get; set; }

        public string Position { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime DateCreated { get; set; }

        public byte[] Salt { get; set; }

        public DataUserModel()
        {
        }

        public DataUserModel(Guid id, string name, string lastname, string email, byte[] password, Enums.UserRole role, Enums.UserDepartment department, string position, string avatarUrl, DateTime dateCreated, byte[] salt)
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
        }
    }
}