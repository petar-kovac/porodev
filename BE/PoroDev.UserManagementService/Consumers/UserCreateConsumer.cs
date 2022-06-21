using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserCreateConsumer : IConsumer<UserCreateRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserCreateConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserCreateRequestGatewayToService> context)
        {
            var modelToReturn = await _userService.CreateUser(context.Message);

            await context.RespondAsync<CommunicationModel<DataUserModel>>(modelToReturn);
        }
    }
}