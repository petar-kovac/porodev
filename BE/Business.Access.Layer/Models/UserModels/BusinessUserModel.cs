using Data.Access.Layer.Models;

namespace Business.Access.Layer.Models.UserModels
{
    public class BusinessUserModel
    {
        public string? Name { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }

        public Enums.UserRole Role { get; set; }

        public Enums.UserDepartment Department { get; set; }

        public Enums.UserPosition Position { get; set; }

        public string? AvatarUrl { get; set; }
    }
}