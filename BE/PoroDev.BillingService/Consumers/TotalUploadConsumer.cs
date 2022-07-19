﻿using MassTransit;
using PoroDev.BillingService.Services.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.BillingService.Consumers
{
    public class TotalUploadConsumer : IConsumer<TotalUploadRequestGatewayToService>
    {
        private readonly IBillingService _billingService;

        public TotalUploadConsumer(IBillingService billingService)
        {
            _billingService = billingService;
        }

        public async Task Consume(ConsumeContext<TotalUploadRequestGatewayToService> context)
        {
            var modelToReturn = await _billingService.TotalUpload(new TotalUploadRequestServiceToDatabase()
            {
                AdminId = context.Message.AdminId,
                UserId = context.Message.UserId
            }) ;

            await context.RespondAsync(modelToReturn);
        }
    }
}