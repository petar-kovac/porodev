using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Consumers
{
    public class TotalRunTimePerMonthConsumer : IConsumer<TotalRunTimePerMonthRequestGatewayToService>
    {

        private readonly IDashboardService _dashboardService;

        public TotalRunTimePerMonthConsumer(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task Consume(ConsumeContext<TotalRunTimePerMonthRequestGatewayToService> context)
        {
            var modelToRead = new TotalRunTimePerMonthRequestServiceToDatabase(context.Message.UserId);

            var modelToReturn = await _dashboardService.GetTotalRunTimePerMonth(modelToRead);

            await context.RespondAsync<CommunicationModel<TotalRunTimePerMonthModel>>(modelToReturn);
        }
    }
}
