using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Consumers
{
    public class TotalNumberOfDeletedFilesConsumer : IConsumer<TotalNumberOfDeletedFilesRequestGatewayToService>
    {
        private readonly IDashboardService _dashboardService;

        public TotalNumberOfDeletedFilesConsumer(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task Consume(ConsumeContext<TotalNumberOfDeletedFilesRequestGatewayToService> context)
        {
            var modelToRead = new TotalNumberOFDeletedFilesRequestServiceToDatabase(context.Message.UserId);

            var modelToReturn = await _dashboardService.GetTotalNumberOfDeletedFiles(modelToRead);

            await context.RespondAsync<CommunicationModel<TotalNumberOfDeletedFilesModel>>(modelToReturn);
        }
    }
}
