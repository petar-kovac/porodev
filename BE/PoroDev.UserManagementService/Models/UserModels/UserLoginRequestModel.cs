namespace PoroDev.UserManagementService.Models.UserModels
{
    public class UserLoginRequestModel
    {
        public UserLoginRequestModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public UserLoginRequestModel()
        {
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}