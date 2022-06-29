using MassTransit;
using Microsoft.IdentityModel.Tokens;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.RunTime.ParametersExecute;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Models.Runtime;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using static PoroDev.GatewayAPI.Constants.Constats;

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
            TokenValidationResult resultOfValidation = await _jwtValidatorService.ValidateRecievedToken(model.Jwt);

            if (!resultOfValidation.IsValid)
            {
                var invalidTokenException = (JWTValidationException)resultOfValidation.Exception;
                invalidTokenException.HumanReadableErrorMessage = CANNOT_VALIDATE_JWT;
                throw invalidTokenException;
            }

            Guid userId = await _jwtValidatorService.GetIdFromToken(resultOfValidation.SecurityToken);

            var modelWithUserId = new ExecuteProjectRequestGatewayToService(userId, model.FileID);

            var requestResponsecontext = await _executeProjet.GetResponse<CommunicationModel<RuntimeData>>(modelWithUserId);

            if (requestResponsecontext.Message.ExceptionName != null)
                ThrowException(requestResponsecontext.Message.ExceptionName, requestResponsecontext.Message.HumanReadableMessage);

            return requestResponsecontext.Message.Entity;
        }

        public async Task<RuntimeData> ExecuteProgramWithArguments(ArgumentListWithJwt model)
        {
            TokenValidationResult resultOfValidation = await _jwtValidatorService.ValidateRecievedToken(model.Jwt);

            if (!resultOfValidation.IsValid)
            {
                var invalidTokenException = (JWTValidationException)resultOfValidation.Exception;
                invalidTokenException.HumanReadableErrorMessage = CANNOT_VALIDATE_JWT;
                throw invalidTokenException;
            }

            Guid userId = await _jwtValidatorService.GetIdFromToken(resultOfValidation.SecurityToken);

            var modelForExecution = new ExecuteProjectWithArgumentsRequestGatewayToService(model.FileID, userId, model.Arguments);

            var requestResponseContext = await _executeWithArguments.GetResponse<CommunicationModel<RuntimeData>>(modelForExecution);

            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            return requestResponseContext.Message.Entity;
        }
    }
}
