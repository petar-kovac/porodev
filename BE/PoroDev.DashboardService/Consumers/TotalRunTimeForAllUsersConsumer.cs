using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Consumers
{
    public class TotalRunTimeForAllUsersConsumer : IConsumer<TotalRunTimeForAllUsersRequestGatewayToService>
    {
        private readonly IDashboardService _dashboardService;

        public TotalRunTimeForAllUsersConsumer(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task Consume(ConsumeContext<TotalRunTimeForAllUsersRequestGatewayToService> context)
        {
            var modelToRead = new TotalRunTimeForAllUsersRequestServiceToDatabase(context.Message.UserId);

            var modelToReturn = await _dashboardService.GetTotalRunTimeForAllUsers(modelToRead);

            await context.RespondAsync<CommunicationModel<TotalRunTimeForAllUsersModel>>(modelToReturn);
        }
    }
}