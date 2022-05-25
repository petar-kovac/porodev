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
    }
}