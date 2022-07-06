using MassTransit;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Runtime.Services.Contracts;

namespace PoroDev.Runtime.Consumers
{
    public class ExecuteProjectConsumer : IConsumer<ExecuteProjectRequestGatewayToService>
    {
        private readonly IRuntimeService _service;

        public ExecuteProjectConsumer(IRuntimeService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<ExecuteProjectRequestGatewayToService> context)
        {
            var result = await _service.ExecuteProject(context.Message.UserId, context.Message.FileID);

            await context.RespondAsync(result);
        }
    }
}