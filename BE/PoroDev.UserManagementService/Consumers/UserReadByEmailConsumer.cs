using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Models.UserModels.Data;
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

            await context.RespondAsync<CommunicationModel<DataUserModel>>(returnUser);
        }
    }
}