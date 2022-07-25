namespace PoroDev.Common.Contracts.UserManagement.Verify
{
    public class VerifyEmailRequestGatewayToService
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}