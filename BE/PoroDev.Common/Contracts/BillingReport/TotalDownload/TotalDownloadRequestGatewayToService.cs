namespace PoroDev.Common.Contracts.BillingReport.TotalDownload
{
    public class TotalDownloadRequestGatewayToService
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public TotalDownloadRequestGatewayToService()
        {
        }

        public TotalDownloadRequestGatewayToService(Guid adminId, Guid userId)
        {
            AdminId = adminId;
            UserId = userId;
        }
    }
}