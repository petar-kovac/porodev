using MassTransit;
using PoroDev.Common.Contracts.UserManagement.Verify;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserVerifyEmailConsumer : IConsumer<VerifyEmailRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserVerifyEmailConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<VerifyEmailRequestGatewayToService> context)
        {
            var modelToReturn = await _userService.VerifyEmail(context.Message);

            await context.RespondAsync(modelToReturn);
        }
    }
}