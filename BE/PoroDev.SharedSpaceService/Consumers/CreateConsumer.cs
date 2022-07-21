using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.SharedSpaceService.Services.Contracts;

namespace PoroDev.SharedSpaceService.Consumers
{
    public class CreateConsumer : IConsumer<CreateSharedSpaceRequestGatewayToService>
    {
        private readonly ISharedSpaceService _sharedSpaceService;

        public CreateConsumer(ISharedSpaceService sharedSpaceService)
        {
            _sharedSpaceService = sharedSpaceService;
        }
        public async Task Consume(ConsumeContext<CreateSharedSpaceRequestGatewayToService> context)
        {
            var modelToReturn = await _sharedSpaceService.Create(context.Message);
            
            await context.RespondAsync<CommunicationModel<SharedSpace>>(modelToReturn);
        }
    }
}
