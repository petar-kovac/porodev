using PoroDev.Common.Enums;

namespace PoroDev.Common.Contracts.UserManagement.Update
{
    public class UserUpdateRequestGatewayToService
    {
        public string AvatarUrl { get; set; }
        public UserEnums.UserDepartment Department { get; set; }
        public string Email { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public UserEnums.UserRole Role { get; set; }
    }
}