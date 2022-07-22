using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.GetAllUsers;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.SharedSpaceService.Services.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;

namespace PoroDev.SharedSpaceService.Consumers
{
    public class GetAllUsersFromSharedSpaceConsumer : IConsumer<GetAllUsersFromSharedSpaceRequestGatewayToService>
    {
        private readonly ISharedSpaceService _sharedSpaceService;

        public GetAllUsersFromSharedSpaceConsumer(ISharedSpaceService sharedSpaceService)
        {
            _sharedSpaceService = sharedSpaceService;
        }

        public async Task Consume(ConsumeContext<GetAllUsersFromSharedSpaceRequestGatewayToService> context)
        {
            var response = await _sharedSpaceService.GetAllUsersFromSharedSpace(context.Message);
            await context.RespondAsync<CommunicationModel<List<DataUserModel>>>(response);
        }
    }
}
