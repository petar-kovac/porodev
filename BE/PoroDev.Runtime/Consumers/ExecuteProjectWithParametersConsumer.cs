using MassTransit;
using PoroDev.Common.Contracts.RunTime.ParametersExecute;
using PoroDev.Runtime.Services.Contracts;

namespace PoroDev.Runtime.Consumers
{
    public class ExecuteProjectWithParametersConsumer : IConsumer<ExecuteProjectWithArgumentsRequestGatewayToService>
    {
        private readonly IRuntimeService _service;

        public ExecuteProjectWithParametersConsumer(IRuntimeService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<ExecuteProjectWithArgumentsRequestGatewayToService> context)
        {
            var result = await _service.ExecuteProject(context.Message.UserId, context.Message.FileId, context.Message.Arguments);

            await context.RespondAsync(result);
        }
    }
}