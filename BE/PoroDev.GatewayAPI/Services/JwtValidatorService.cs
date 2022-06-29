using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class JwtValidatorService : IJwtValidatorService
    {
        private readonly IRequestClient<UserReadByIdRequestGatewayToService> _readUserById;

        public JwtValidatorService(IRequestClient<UserReadByIdRequestGatewayToService> readUserById)
        {
            _readUserById = readUserById;
        }

        public async Task<Guid> ValidateToken(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(jwt);

            var id = Guid.Parse(jsonToken.Claims.First(claim => claim.Type == "Id").Value);

            var readUserByIdResponseContext = await _readUserById.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService(id));
            if (readUserByIdResponseContext.Message.ExceptionName != null)
                ThrowException(readUserByIdResponseContext.Message.ExceptionName, readUserByIdResponseContext.Message.HumanReadableMessage);

            return id;
        }
    }
}
