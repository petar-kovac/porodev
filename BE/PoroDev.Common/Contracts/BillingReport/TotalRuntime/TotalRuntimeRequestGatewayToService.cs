namespace PoroDev.Common.Contracts.BillingReport.TotalRuntime
{
    public class TotalRuntimeRequestGatewayToService
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public TotalRuntimeRequestGatewayToService()
        {
        }

        public TotalRuntimeRequestGatewayToService(Guid adminId, Guid userId)
        {
            AdminId = adminId;
            UserId = userId;
        }
    }
}