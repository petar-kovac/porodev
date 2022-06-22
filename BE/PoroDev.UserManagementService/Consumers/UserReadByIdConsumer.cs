using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserReadByIdConsumer : IConsumer<UserReadByIdRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserReadByIdConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserReadByIdRequestGatewayToService> context)
        {
            var returnuser = await _userService.ReadUserById(context.Message);

            await context.RespondAsync<CommunicationModel<DataUserModel>>(returnuser);
        }
    }
}
