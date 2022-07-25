namespace PoroDev.Common.Contracts.BillingReport.TotalRuntime
{
    public class TotalRuntimeRequestGatewayToService
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public string Month { get; init; }

        public TotalRuntimeRequestGatewayToService()
        {
        }

        public TotalRuntimeRequestGatewayToService(Guid adminId, Guid userId, string month)
        {
            AdminId = adminId;
            UserId = userId;
            Month = month;
        }
    }
}