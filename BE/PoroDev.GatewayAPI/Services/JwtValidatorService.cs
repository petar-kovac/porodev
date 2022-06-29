using MassTransit;
using Microsoft.IdentityModel.Tokens;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using static PoroDev.GatewayAPI.Constants.Constats;
using PoroDev.Common.Exceptions;

namespace PoroDev.GatewayAPI.Services
{
    public class JwtValidatorService : IJwtValidatorService
    {
        private readonly IRequestClient<UserReadByIdRequestGatewayToService> _readUserById;

        public JwtValidatorService(IRequestClient<UserReadByIdRequestGatewayToService> readUserById)
        {
            _readUserById = readUserById;
        }

        public async Task<Guid> GetIdFromToken(SecurityToken securityToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = securityToken as JwtSecurityToken;

            var id = Guid.Parse(jsonToken.Claims.First(claim => claim.Type == "Id").Value);

            var readUserByIdResponseContext = await _readUserById.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService(id));
            if (readUserByIdResponseContext.Message.ExceptionName != null)
                ThrowException(readUserByIdResponseContext.Message.ExceptionName, readUserByIdResponseContext.Message.HumanReadableMessage);

            return id;
        }

        public async Task<TokenValidationResult> ValidateRecievedToken(string jwtForValidation)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var validator = await tokenHandler.ValidateTokenAsync(jwtForValidation, validationParameters);

            return validator;
        }
    }
}
