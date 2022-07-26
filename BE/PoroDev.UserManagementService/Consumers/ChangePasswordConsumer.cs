using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ChangePassword;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class ChangePasswordConsumer : IConsumer<ChangePasswordRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public ChangePasswordConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<ChangePasswordRequestGatewayToService> context)
        {
            var response = await _userService.ChangePassword(context.Message);
            await context.RespondAsync<CommunicationModel<DataUserModel>>(response);
        }
    }
}
