namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalRunTimePerMonthRequestGatewayToService()
        {
        }

        public TotalRunTimePerMonthRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}