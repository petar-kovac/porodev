namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase()
        {
        }

        public TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}