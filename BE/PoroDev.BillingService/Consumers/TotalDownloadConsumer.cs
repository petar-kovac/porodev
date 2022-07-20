using MassTransit;
using PoroDev.BillingService.Services.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalDownload;

namespace PoroDev.BillingService.Consumers
{
    public class TotalDownloadConsumer : IConsumer<TotalDownloadRequestGatewayToService>
    {
        private readonly IBillingService _billingService;

        public TotalDownloadConsumer(IBillingService billingService)
        {
            _billingService = billingService;
        }

        public async Task Consume(ConsumeContext<TotalDownloadRequestGatewayToService> context)
        {
            var modelToReturn = await _billingService.TotalDownload(new TotalDownloadRequestServiceToDatabase()
            {
                AdminId = context.Message.AdminId,
                UserId = context.Message.UserId
            });

            await context.RespondAsync(modelToReturn);
        }
    }
}
