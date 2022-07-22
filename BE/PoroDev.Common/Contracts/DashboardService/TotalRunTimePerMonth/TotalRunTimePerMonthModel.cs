namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthModel
    {
        public string Month { get; set; }

        public ulong TotalRunTime { get; set; }

        public TotalRunTimePerMonthModel()
        {
        }

        public TotalRunTimePerMonthModel(string month, ushort totalRunTime)
        {
            Month = month;
            TotalRunTime = totalRunTime;
        }
    }
}