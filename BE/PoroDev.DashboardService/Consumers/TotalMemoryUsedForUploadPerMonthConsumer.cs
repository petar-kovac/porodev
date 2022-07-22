using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Consumers
{
    public class TotalMemoryUsedForUploadPerMonthConsumer : IConsumer<TotalMemoryUsedForUploadPerMonthRequestGatewayToService>
    {
        private readonly IDashboardService _dashboardService;

        public TotalMemoryUsedForUploadPerMonthConsumer(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task Consume(ConsumeContext<TotalMemoryUsedForUploadPerMonthRequestGatewayToService> context)
        {
            var modelToRead = new TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase(context.Message.UserId);

            var modelToReturn = await _dashboardService.GetTotalMemoryUsedForUploadPerMonth(modelToRead);

            await context.RespondAsync<CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>>(modelToReturn);
        }
    }
}
