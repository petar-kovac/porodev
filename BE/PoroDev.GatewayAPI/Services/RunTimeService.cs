using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.RunTime.ParametersExecute;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Models.Runtime;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class RunTimeService : IRunTimeService
    {
        private readonly IRequestClient<ExecuteProjectRequestGatewayToService> _executeProjet;
        private readonly IRequestClient<ExecuteProjectWithArgumentsRequestGatewayToService> _executeWithArguments;

        public RunTimeService(
            IRequestClient<ExecuteProjectRequestGatewayToService> executeProjet,
            IRequestClient<ExecuteProjectWithArgumentsRequestGatewayToService> executeWithArguments
            )
        {
            _executeProjet = executeProjet;
            _executeWithArguments = executeWithArguments;
        }

        public async Task<RuntimeData> ExecuteProgram(ArgumentListWithUserId model)
        {
            if (model.Arguments.Count == 0)
            {
                var modelWithoutArguments = new ExecuteProjectRequestGatewayToService(model.UserId, model.FileID);
                return await ExecuteProgram(modelWithoutArguments);
            }
            else
            {
                var modelWithArguments = new ExecuteProjectWithArgumentsRequestGatewayToService(model.FileID, model.UserId, model.Arguments);
                return await ExecuteProgramWithArguments(modelWithArguments);
            }
        }

        private async Task<RuntimeData> ExecuteProgram(ExecuteProjectRequestGatewayToService model)
        {
            var requestResponsecontext = await _executeProjet.GetResponse<CommunicationModel<RuntimeData>>(model, CancellationToken.None, RequestTimeout.After(m: 5));

            if (requestResponsecontext.Message.ExceptionName != null)
                ThrowException(requestResponsecontext.Message.ExceptionName, requestResponsecontext.Message.HumanReadableMessage);

            return requestResponsecontext.Message.Entity;
        }

        private async Task<RuntimeData> ExecuteProgramWithArguments(ExecuteProjectWithArgumentsRequestGatewayToService model)
        {
            var requestResponseContext = await _executeWithArguments.GetResponse<CommunicationModel<RuntimeData>>(model, CancellationToken.None, RequestTimeout.After(m: 5));

            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            return requestResponseContext.Message.Entity;
        }
    }
}