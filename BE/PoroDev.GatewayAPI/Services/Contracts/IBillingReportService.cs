using PoroDev.Common.Contracts.BillingReport.TotalDownload;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IBillingReportService
    {
        Task<TotalUploadResponse> TotalUpload(TotalUploadRequestGatewayToService uploadModel);

        Task<TotalDownloadResponse> TotalDownload(TotalDownloadRequestGatewayToService downloadModel);

        Task<TotalRuntimeResponse> TotalRuntime(TotalRuntimeRequestGatewayToService runtimeModel);
    }
}