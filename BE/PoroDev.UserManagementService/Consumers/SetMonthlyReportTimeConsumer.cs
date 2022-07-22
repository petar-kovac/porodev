using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.SetMonthlyReportTime;
using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class SetMonthlyReportTimeConsumer : IConsumer<SetMonthlyReportTimeRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public SetMonthlyReportTimeConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<SetMonthlyReportTimeRequestGatewayToService> context)
        {
            var modelToReturn = await _userService.SetMonthlyReportTime(context.Message);
            await context.RespondAsync<CommunicationModel<NotificationDataModel>>(modelToReturn);
        }
    }
}
