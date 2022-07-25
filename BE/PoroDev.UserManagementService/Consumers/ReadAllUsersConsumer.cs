using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadAllUsers;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class ReadAllUsersConsumer : IConsumer<ReadAllUsersRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public ReadAllUsersConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<ReadAllUsersRequestGatewayToService> context)
        {
            var responseContext = await _userService.ReadAllUsers(context.Message);
            await context.RespondAsync<CommunicationModel<List<DataUserModel>>>(responseContext);
        }
    }
}
