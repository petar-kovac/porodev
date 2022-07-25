namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalRunTimePerMonthRequestServiceToDatabase()
        {
        }

        public TotalRunTimePerMonthRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}