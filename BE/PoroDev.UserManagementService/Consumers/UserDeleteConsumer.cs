using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserDeleteConsumer : IConsumer<UserDeleteRequestGatewayToService>
    {

        private readonly IUserService _userService;

        public UserDeleteConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserDeleteRequestGatewayToService> context)
        {
            var modelToReturn = await _userService.DeleteUser(context.Message);

            await context.RespondAsync<CommunicationModel<DeleteUserModel>>(modelToReturn);
        }
    }
}
