namespace PoroDev.Common.Contracts.UserManagement.Verify
{
    public class VerifyEmailRequestServiceToDatabase
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}