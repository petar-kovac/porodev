namespace PoroDev.Common.Contracts.BillingReport.TotalUpload
{
    public class TotalUploadRequestServiceToDatabase
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public string Month { get; init; }

        public TotalUploadRequestServiceToDatabase()
        {
        }

        public TotalUploadRequestServiceToDatabase(Guid adminId, Guid userId, string month)
        {
            AdminId = adminId;
            UserId = userId;
            Month = month;
        }
    }
}