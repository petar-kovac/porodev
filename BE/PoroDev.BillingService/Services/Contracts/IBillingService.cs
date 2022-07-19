using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;

namespace PoroDev.BillingService.Services.Contracts
{
    public interface IBillingService
    {
        Task<CommunicationModel<TotalUploadResponse>> TotalUpload(TotalUploadRequestServiceToDatabase totalUploadRequestServiceToDatabase);

    }
}
