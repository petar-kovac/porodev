using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.SharedSpaceService.Services.Contracts;

namespace PoroDev.SharedSpaceService.Consumers
{
    public class AddUserToSharedSpaceConsumer : IConsumer<AddUserToSharedSpaceRequestGatewayToService>
    {
        private readonly ISharedSpaceService _sharedSpaceService;

        public AddUserToSharedSpaceConsumer(ISharedSpaceService sharedSpaceService)
        {
            _sharedSpaceService = sharedSpaceService;
        }

        public async Task Consume(ConsumeContext<AddUserToSharedSpaceRequestGatewayToService> context)
        {
            var response = await _sharedSpaceService.AddUserToSharedSpace(context.Message);
            await context.RespondAsync<CommunicationModel<SharedSpacesUsers>>(response);
        }
    }
}
