using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.DeleteAllUsers;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserDeleteAllConsumer : IConsumer<UserDeleteAllRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserDeleteAllConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserDeleteAllRequestGatewayToService> context)
        {
            var modelToReturn = await _userService.DeleteAllUsers(context.Message);

            await context.RespondAsync<CommunicationModel<DeleteUserModel>>(modelToReturn);
        }
    }
}