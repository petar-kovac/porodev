namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthModel
    {
       public List<TotalMemoryUsedForDownloadPerMonthSingleModel> Content { get; set; }
        
        public TotalMemoryUsedForDownloadPerMonthModel()
        {
            Content = new List<TotalMemoryUsedForDownloadPerMonthSingleModel>();
        }

        public TotalMemoryUsedForDownloadPerMonthModel(List<TotalMemoryUsedForDownloadPerMonthSingleModel> content)
        {
            Content = content;
        }
    }
}