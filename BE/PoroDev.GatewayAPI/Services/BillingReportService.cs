using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class BillingReportService : IBillingReportService
    {
        private readonly IRequestClient<TotalUploadRequestGatewayToService> _uploadRequestClient;

        public BillingReportService
            (IRequestClient<TotalUploadRequestGatewayToService> uploadRequestClient)
        {
            _uploadRequestClient = uploadRequestClient;
        }

        public async Task<TotalUploadResponse> TotalUpload(TotalUploadRequestGatewayToService uploadModel)
        {
            var responseContext = await _uploadRequestClient.GetResponse<CommunicationModel<TotalUploadResponse>>(uploadModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }
            var response = responseContext.Message.Entity;

            return response;
        }
    }
}
