using MassTransit;
using Microsoft.IdentityModel.Tokens;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using static PoroDev.Common.Constants.Constants;
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

        private async Task<Guid> GetIdFromToken(SecurityToken securityToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = securityToken as JwtSecurityToken;

            var id = Guid.Parse(jsonToken.Claims.First(claim => claim.Type == "Id").Value);

            var readUserByIdResponseContext = await _readUserById.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService(id));
            if (readUserByIdResponseContext.Message.ExceptionName != null)
                ThrowException(readUserByIdResponseContext.Message.ExceptionName, readUserByIdResponseContext.Message.HumanReadableMessage);

            return id;
        }

        public async Task<Guid> ValidateRecievedToken(string jwtForValidation) 
        {
            if (jwtForValidation.Count() == 0)
                ThrowException(nameof(NoHeaderWithJwtException), "There is no JWT in request's header.");

            string accessTokenWithoutBearerPrefix = jwtForValidation.Substring("Bearer ".Length);

            TokenValidationResult resultOfValidation = await ValidateToken(accessTokenWithoutBearerPrefix);

            if (!resultOfValidation.IsValid)
            {
                var invalidTokenException = new JWTValidationException()
                {
                    HumanReadableErrorMessage = CANNOT_VALIDATE_JWT
                };
                throw invalidTokenException;
            }

            Guid userId = await GetIdFromToken(resultOfValidation.SecurityToken);

            return userId;

        }

        private async Task<TokenValidationResult> ValidateToken(string jwtForValidation)
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
