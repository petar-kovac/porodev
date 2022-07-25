namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers
{
    public class TotalNumberOfUsersRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalNumberOfUsersRequestGatewayToService()
        {
        }

        public TotalNumberOfUsersRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}