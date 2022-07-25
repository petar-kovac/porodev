using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalDownload;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;

namespace PoroDev.BillingService.Services.Contracts
{
    public interface IBillingService
    {
        Task<CommunicationModel<TotalUploadResponse>> TotalUpload(TotalUploadRequestServiceToDatabase uploadModel);

        Task<CommunicationModel<TotalDownloadResponse>> TotalDownload(TotalDownloadRequestServiceToDatabase downloadModel);

        Task<CommunicationModel<TotalRuntimeResponse>> TotalRuntime(TotalRuntimeRequestServiceToDatabase runtimeModel);
    }
}