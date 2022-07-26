namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public int NumberOfMonthsToShow { get; set; }


        public TotalRunTimePerMonthRequestGatewayToService()
        {
        }

        public TotalRunTimePerMonthRequestGatewayToService(Guid userId, int numberOfMonthsToShow)
        {
            UserId = userId;
            NumberOfMonthsToShow = numberOfMonthsToShow;
        }
    }
}