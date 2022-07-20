namespace PoroDev.Common.Contracts.BillingReport.TotalRuntime
{
    public class TotalRuntimeRequestServiceToDatabase
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public TotalRuntimeRequestServiceToDatabase()
        {
        }

        public TotalRuntimeRequestServiceToDatabase(Guid adminId, Guid userId)
        {
            AdminId = adminId;
            UserId = userId;
        }
    }
}