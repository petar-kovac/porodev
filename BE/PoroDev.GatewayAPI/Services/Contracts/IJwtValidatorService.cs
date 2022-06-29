namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IJwtValidatorService
    {
        Task<Guid> ValidateToken(string jwt);
    }
}
