namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalMemoryUsedForUploadPerMonthRequestGatewayToService()
        {
        }

        public TotalMemoryUsedForUploadPerMonthRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}