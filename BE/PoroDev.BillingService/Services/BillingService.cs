using MassTransit;
using PoroDev.BillingService.Services.Contracts;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalDownload;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;

namespace PoroDev.BillingService.Services
{
    public class BillingService : IBillingService
    {
        private readonly IRequestClient<TotalUploadRequestServiceToDatabase> _uploadRequestClient;
        private readonly IRequestClient<TotalDownloadRequestServiceToDatabase> _downloadRequestClient;
        private readonly IRequestClient<TotalRuntimeRequestServiceToDatabase> _runtimeRequestClient;

        public BillingService
            (IRequestClient<TotalUploadRequestServiceToDatabase> uploadRequestClient,
            IRequestClient<TotalDownloadRequestServiceToDatabase> downloadRequestClient,
            IRequestClient<TotalRuntimeRequestServiceToDatabase> runtimeRequestClient)
        {
            _uploadRequestClient = uploadRequestClient;
            _downloadRequestClient = downloadRequestClient;
            _runtimeRequestClient = runtimeRequestClient;
        }

        public async Task<CommunicationModel<TotalUploadResponse>> TotalUpload(TotalUploadRequestServiceToDatabase uploadModel)
        {
            var response = await _uploadRequestClient.GetResponse<CommunicationModel<TotalUploadResponse>>(uploadModel);

            return response.Message;
        }

        public async Task<CommunicationModel<TotalDownloadResponse>> TotalDownload(TotalDownloadRequestServiceToDatabase downloadModel)
        {
            var response = await _downloadRequestClient.GetResponse<CommunicationModel<TotalDownloadResponse>>(downloadModel);

            return response.Message;
        }

        public async Task<CommunicationModel<TotalRuntimeResponse>> TotalRuntime(TotalRuntimeRequestServiceToDatabase runtimeModel)
        {
            var response = await _runtimeRequestClient.GetResponse<CommunicationModel<TotalRuntimeResponse>>(runtimeModel);

            return response.Message;
        }
    }
}