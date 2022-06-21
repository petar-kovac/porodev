using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserLoginConsumer : IConsumer<UserLoginRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserLoginConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserLoginRequestGatewayToService> context)
        {
            var modelToReturn = await _userService.LoginUser(context.Message);
            await context.RespondAsync<CommunicationModel<LoginUserModel>>(modelToReturn);
        }
    }
}