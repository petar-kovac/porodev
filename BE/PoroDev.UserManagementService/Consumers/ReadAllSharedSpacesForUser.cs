using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadAllSharedSpacesForUser;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class ReadAllSharedSpacesForUser : IConsumer<ReadAllSharedSpacesForUserRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public ReadAllSharedSpacesForUser(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<ReadAllSharedSpacesForUserRequestGatewayToService> context)
        {
            var returnModel = await _userService.ReadAllSharedSpacesForUser(context.Message);
            await context.RespondAsync<CommunicationModel<List<SharedSpace>>>(returnModel);
        }
    }
}
