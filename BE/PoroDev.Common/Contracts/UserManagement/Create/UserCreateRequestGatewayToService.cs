using PoroDev.Common.Enums;

namespace PoroDev.Common.Contracts.UserManagement.Create
{
    public class UserCreateRequestGatewayToService
    {
        public string AvatarUrl { get; set; }
        public UserEnums.UserDepartment Department { get; set; }
        public string Email { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string PasswordUnhashed { get; set; }
        public string Position { get; set; }
        public UserEnums.UserRole Role { get; set; }

        public string VerificationToken { get; set; }
    }
}