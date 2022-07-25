using MassTransit;
using PoroDev.Common.Contracts.UserManagement.Query;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers.Query
{
    public class UserCreateConsumer : IConsumer<QueryAllUsersRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserCreateConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<QueryAllUsersRequestGatewayToService> context)
        {
            var response = await _userService.QueryAll();

            await context.RespondAsync(response);
        }
    }
}