namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IJwtValidatorService
    {
        Task<Guid> ValidateRecievedToken(string jwtForValidation);
    }
}