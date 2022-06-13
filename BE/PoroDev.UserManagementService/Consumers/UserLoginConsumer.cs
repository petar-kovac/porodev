using MassTransit;
using PoroDev.Common.Contracts.LoginUser;
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

        public Task Consume(ConsumeContext<UserLoginRequestGatewayToService> context)
        {
            throw new NotImplementedException();
        }
    }
}
