using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Consumers
{
    public class TotalNumberOfUploadedFilesConsumer : IConsumer<TotalNumberOfUploadedFilesRequestGatewayToService>
    {
        private readonly IDashboardService _dashboardService;

        public TotalNumberOfUploadedFilesConsumer(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task Consume(ConsumeContext<TotalNumberOfUploadedFilesRequestGatewayToService> context)
        {
            var modelToRead = new TotalNumberOfUploadedFilesRequestServiceToDatabase(context.Message.UserId);

            var modelToReturn = await _dashboardService.GetTotalNumberOfUploadedFiles(modelToRead);

            await context.RespondAsync<CommunicationModel<TotalNumberOfUploadedFilesModel>>(modelToReturn);
        }
    }
}
