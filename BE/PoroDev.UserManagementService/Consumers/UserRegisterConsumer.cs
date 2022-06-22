using MassTransit;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserRegisterConsumer : IConsumer<RegisterUserRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserRegisterConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<RegisterUserRequestGatewayToService> context)
        {
            var response = await _userService.RegisterUser(context.Message);
            await context.RespondAsync(response);
        }
    }
}
