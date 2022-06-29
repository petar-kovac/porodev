using Microsoft.IdentityModel.Tokens;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IJwtValidatorService
    {
        Task<Guid> GetIdFromToken(SecurityToken securityToken);

        Task<TokenValidationResult> ValidateRecievedToken(string jwtForValidation);
    }
}
