using MassTransit;
using PoroDev.BillingService.Services.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;

namespace PoroDev.BillingService.Consumers
{
    public class TotalRuntimeConsumer : IConsumer<TotalRuntimeRequestGatewayToService>
    {
        private readonly IBillingService _billingService;

        public TotalRuntimeConsumer(IBillingService billingService)
        {
            _billingService = billingService;
        }

        public async Task Consume(ConsumeContext<TotalRuntimeRequestGatewayToService> context)
        {
            var modelToReturn = await _billingService.TotalRuntime(new TotalRuntimeRequestServiceToDatabase()
            {
                AdminId = context.Message.AdminId,
                UserId = context.Message.UserId
            });

            await context.RespondAsync(modelToReturn);
        }
    }
}