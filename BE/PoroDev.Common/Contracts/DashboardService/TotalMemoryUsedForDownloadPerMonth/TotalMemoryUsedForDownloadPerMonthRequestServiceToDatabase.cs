namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase()
        {
        }

        public TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}