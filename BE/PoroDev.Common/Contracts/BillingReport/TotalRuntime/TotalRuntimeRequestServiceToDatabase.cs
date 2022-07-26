namespace PoroDev.Common.Contracts.BillingReport.TotalRuntime
{
    public class TotalRuntimeRequestServiceToDatabase
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public string Month { get; init; }

        public TotalRuntimeRequestServiceToDatabase()
        {
        }

        public TotalRuntimeRequestServiceToDatabase(Guid adminId, Guid userId, string month)
        {
            AdminId = adminId;
            UserId = userId;
            Month = month;
        }
    }
}