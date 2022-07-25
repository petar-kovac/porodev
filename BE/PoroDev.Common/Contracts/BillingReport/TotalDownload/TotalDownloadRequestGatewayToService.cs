namespace PoroDev.Common.Contracts.BillingReport.TotalDownload
{
    public class TotalDownloadRequestGatewayToService
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public string Month { get; init; }

        public TotalDownloadRequestGatewayToService()
        {
        }

        public TotalDownloadRequestGatewayToService(Guid adminId, Guid userId, string month)
        {
            AdminId = adminId;
            UserId = userId;
            Month = month;
        }
    }
}