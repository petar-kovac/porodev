using MassTransit;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserReadByEmailConsumer : IConsumer<UserReadByEmailRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserReadByEmailConsumer(IUserService userService)
        {
            _userService = userService;
        }
        public async Task Consume(ConsumeContext<UserReadByEmailRequestGatewayToService> context)
        {
            var returnUser = await _userService.ReadUserByEmail(context.Message);

            await context.RespondAsync<UserReadByEmailResponseServiceToGateway>(returnUser);
        }
    }
}
