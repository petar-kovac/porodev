using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Consumers
{
    public class TotalMemoryUsedForDownloadPerMonthConsumer : IConsumer<TotalMemoryUsedForDownloadPerMonthRequestGatewayToService>
    {
        private readonly IDashboardService _dashboardService;

        public TotalMemoryUsedForDownloadPerMonthConsumer(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task Consume(ConsumeContext<TotalMemoryUsedForDownloadPerMonthRequestGatewayToService> context)
        {
            var modelToRead = new TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase(context.Message.UserId);

            var modelToReturn = await _dashboardService.GetTotalMemoryUsedForDownloadPerMonth(modelToRead);

            await context.RespondAsync<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>>(modelToReturn);
        }
    }
}