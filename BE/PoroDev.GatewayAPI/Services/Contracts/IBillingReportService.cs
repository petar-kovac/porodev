using PoroDev.Common.Contracts.BillingReport.TotalUpload;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IBillingReportService
    {
        Task<TotalUploadResponse> TotalUpload(TotalUploadRequestGatewayToService uploadModel);
    }
}
