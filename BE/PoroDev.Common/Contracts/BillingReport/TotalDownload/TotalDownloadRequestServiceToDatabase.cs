namespace PoroDev.Common.Contracts.BillingReport.TotalDownload
{
    public class TotalDownloadRequestServiceToDatabase
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public string Month { get; init; }

        public TotalDownloadRequestServiceToDatabase()
        {
        }

        public TotalDownloadRequestServiceToDatabase(Guid adminId, Guid userId, string month)
        {
            AdminId = adminId;
            UserId = userId;
            Month = month;
        }
    }
}