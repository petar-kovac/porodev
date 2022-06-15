using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserUpdateConsumer : IConsumer<UserUpdateRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserUpdateConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserUpdateRequestGatewayToService> context)
        {
            var modelToReturn = await _userService.UpdateUser(context.Message);
          
            await context.RespondAsync(modelToReturn);
        }
    }
}