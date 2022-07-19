using MassTransit;
using PoroDev.BillingService.Services.Contracts;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;

namespace PoroDev.BillingService.Services
{
    public class BillingService : IBillingService
    {
        private readonly IRequestClient<TotalUploadRequestGatewayToService> _uploadRequestClient;

        public BillingService
            (IRequestClient<TotalUploadRequestGatewayToService> uploadRequestClient)
        {
            _uploadRequestClient = uploadRequestClient;
        }

        public async Task<CommunicationModel<TotalUploadResponse>> TotalUpload(TotalUploadRequestServiceToDatabase uploadModel)
        {
            var response = await _uploadRequestClient.GetResponse<CommunicationModel<TotalUploadResponse>>(uploadModel);

            return response.Message;
        }
    }
}
