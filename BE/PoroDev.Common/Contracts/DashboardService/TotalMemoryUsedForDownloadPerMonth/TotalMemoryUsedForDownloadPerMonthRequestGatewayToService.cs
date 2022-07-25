namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalMemoryUsedForDownloadPerMonthRequestGatewayToService()
        {
        }

        public TotalMemoryUsedForDownloadPerMonthRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}