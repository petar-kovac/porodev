using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalDownload;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class BillingReportService : IBillingReportService
    {
        private readonly IRequestClient<TotalUploadRequestGatewayToService> _uploadRequestClient;
        private readonly IRequestClient<TotalDownloadRequestGatewayToService> _downloadRequestClient;
        private readonly IRequestClient<TotalRuntimeRequestGatewayToService> _runtimeRequestClient;

        public BillingReportService
            (IRequestClient<TotalUploadRequestGatewayToService> uploadRequestClient,
            IRequestClient<TotalDownloadRequestGatewayToService> downloadRequestClient,
            IRequestClient<TotalRuntimeRequestGatewayToService> runtimeRequestClient)
        {
            _uploadRequestClient = uploadRequestClient;
            _downloadRequestClient = downloadRequestClient;
            _runtimeRequestClient = runtimeRequestClient;
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

        public async Task<TotalDownloadResponse> TotalDownload(TotalDownloadRequestGatewayToService downloadModel)
        {
            var responseContext = await _downloadRequestClient.GetResponse<CommunicationModel<TotalDownloadResponse>>(downloadModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }
            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalRuntimeResponse> TotalRuntime(TotalRuntimeRequestGatewayToService runtimeModel)
        {
            var responseContext = await _runtimeRequestClient.GetResponse<CommunicationModel<TotalRuntimeResponse>>(runtimeModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }
            var response = responseContext.Message.Entity;

            return response;
        }
    }
}