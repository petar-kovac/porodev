using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Consumers
{
    public class TotalNumberOfUsersConsumer : IConsumer<TotalNumberOfUsersRequestGatewayToService>
    {
        private readonly IDashboardService _dashboardService;

        public TotalNumberOfUsersConsumer(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task Consume(ConsumeContext<TotalNumberOfUsersRequestGatewayToService> context)
        {
            var modelToRead = new TotalNumberOfUsersRequestServiceToDatabase(context.Message.UserId);

            var modelToReturn = await _dashboardService.GetTotalNumberOfUsers(modelToRead);

            await context.RespondAsync<CommunicationModel<TotalNumberOfUsersModel>>(modelToReturn);
        }
    }
}