namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers
{
    public class TotalRunTimeForAllUsersRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalRunTimeForAllUsersRequestGatewayToService()
        {
        }

        public TotalRunTimeForAllUsersRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}