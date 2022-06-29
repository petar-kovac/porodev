using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.RunTime.ParametersExecute;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class RunTimeService : IRunTimeService
    {
        private readonly IRequestClient<ExecuteProjectRequestGatewayToService> _executeProjet;     
        private readonly IRequestClient<ExecuteProjectWithArgumentsRequestGatewayToService> _executeWithArguments;
        private readonly IJwtValidatorService _jwtValidatorService;

        public RunTimeService(
            IRequestClient<ExecuteProjectRequestGatewayToService> executeProjet, 
            IRequestClient<ExecuteProjectWithArgumentsRequestGatewayToService> executeWithArguments,
            IJwtValidatorService jwtValidatorService
            )
        {
            _executeProjet = executeProjet;
            _executeWithArguments = executeWithArguments;
            _jwtValidatorService = jwtValidatorService;
        }

        public async Task<RuntimeData> ExecuteProgram(ExecuteProjectRequestClientToGateway model)
        {
            Guid userId = await _jwtValidatorService.ValidateToken(model.Jwt);

            var modelWithUserId = new ExecuteProjectRequestGatewayToService(userId, model.FileID);

            var requestResponsecontext = await _executeProjet.GetResponse<CommunicationModel<RuntimeData>>(modelWithUserId);

            if (requestResponsecontext.Message.ExceptionName != null)
                ThrowException(requestResponsecontext.Message.ExceptionName, requestResponsecontext.Message.HumanReadableMessage);

            return requestResponsecontext.Message.Entity;
        }

        public async Task<RuntimeData> ExecuteProgramWithArguments(ArgumentListWithJwt model)
        {
            Guid userId = await _jwtValidatorService.ValidateToken(model.Jwt);

            var modelForExecution = new ExecuteProjectWithArgumentsRequestGatewayToService(model, userId);

            var requestResponseContext = await _executeWithArguments.GetResponse<CommunicationModel<RuntimeData>>(modelForExecution);

            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            return requestResponseContext.Message.Entity;
        }
    }
}
