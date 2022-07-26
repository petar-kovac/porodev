namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthModel
    {
        public List<TotalRunTimePerMonthSingleModel> Content { get; set; }

        public TotalRunTimePerMonthModel()
        {
            Content = new List<TotalRunTimePerMonthSingleModel>();
        }

        public TotalRunTimePerMonthModel(List<TotalRunTimePerMonthSingleModel> content)
        {
            Content = content;
        }
    }
}