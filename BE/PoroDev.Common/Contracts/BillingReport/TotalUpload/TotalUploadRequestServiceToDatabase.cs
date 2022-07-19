namespace PoroDev.Common.Contracts.BillingReport.TotalUpload
{
    public class TotalUploadRequestServiceToDatabase
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public TotalUploadRequestServiceToDatabase()
        {
        }

        public TotalUploadRequestServiceToDatabase(Guid adminId, Guid userId)
        {
            AdminId = adminId;
            UserId = userId;
        }
    }
}