using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserReadByIdConsumerWithRuntime : IConsumer<UserReadByIdWithRuntimeRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserReadByIdConsumerWithRuntime(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserReadByIdWithRuntimeRequestGatewayToService> context)
        {
            var returnuser = await _userService.ReadUserByIdWithRuntimeData(context.Message);

            await context.RespondAsync(returnuser);
        }
    }
}
