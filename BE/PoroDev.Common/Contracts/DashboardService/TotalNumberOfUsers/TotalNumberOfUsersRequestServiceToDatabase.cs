namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers
{
    public class TotalNumberOfUsersRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalNumberOfUsersRequestServiceToDatabase()
        {
        }

        public TotalNumberOfUsersRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}