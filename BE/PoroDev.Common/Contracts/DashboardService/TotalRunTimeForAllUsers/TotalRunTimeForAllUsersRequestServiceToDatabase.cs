namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers
{
    public class TotalRunTimeForAllUsersRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalRunTimeForAllUsersRequestServiceToDatabase()
        {
        }

        public TotalRunTimeForAllUsersRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}