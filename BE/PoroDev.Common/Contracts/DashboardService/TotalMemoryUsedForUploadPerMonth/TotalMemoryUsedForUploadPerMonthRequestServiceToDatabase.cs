namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public int NumberOfMonthsToShow { get; set; }

        public TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase()
        {
        }

        public TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase(Guid userId, int numberOfMonthsToShow)
        {
            UserId = userId;
            NumberOfMonthsToShow = numberOfMonthsToShow;
        }
    }
}