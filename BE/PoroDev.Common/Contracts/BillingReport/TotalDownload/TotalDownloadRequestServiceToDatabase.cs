namespace PoroDev.Common.Contracts.BillingReport.TotalDownload
{
    public class TotalDownloadRequestServiceToDatabase
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public TotalDownloadRequestServiceToDatabase()
        {
        }

        public TotalDownloadRequestServiceToDatabase(Guid adminId, Guid userId)
        {
            AdminId = adminId;
            UserId = userId;
        }
    }
}