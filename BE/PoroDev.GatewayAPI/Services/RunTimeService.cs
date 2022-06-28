using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class RunTimeService : IRunTimeService
    {
        private readonly IRequestClient<ExecuteProjectRequestGatewayToService> _executeProjet;
        private readonly IRequestClient<UserReadByIdRequestGatewayToService> _readUserById;

        public RunTimeService(IRequestClient<ExecuteProjectRequestGatewayToService> executeProjet, IRequestClient<UserReadByIdRequestGatewayToService> readUserById)
        {
            _executeProjet = executeProjet;
            _readUserById = readUserById;
        }

        public async Task<RuntimeData> ExecuteProgram(ExecuteProjectRequestClientToGateway model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(model.Jwt);
            var tokenS = jsonToken as JwtSecurityToken;

            var id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var readUserByIdResponseContext = await _readUserById.GetResponse<CommunicationModel<DataUserModel>>(new UserReadByIdRequestGatewayToService() { Id = Guid.Parse(id) });
            if (readUserByIdResponseContext.Message.ExceptionName != null)
                ThrowException(readUserByIdResponseContext.Message.ExceptionName, readUserByIdResponseContext.Message.HumanReadableMessage);

            var modelWithUserId = new ExecuteProjectRequestGatewayToService() { UserId = Guid.Parse(id), FileID = Guid.Parse(model.FileID) };

            var requestResponsecontext = await _executeProjet.GetResponse<CommunicationModel<RuntimeData>>(modelWithUserId);
            if (requestResponsecontext.Message.ExceptionName != null)
                ThrowException(requestResponsecontext.Message.ExceptionName, requestResponsecontext.Message.HumanReadableMessage);

            return requestResponsecontext.Message.Entity;
        }
    }
}
