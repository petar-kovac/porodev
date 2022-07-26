namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthModel
    {
        public List<TotalMemoryUsedForUploadPerMonthSingleModel> Content { get; set; }

        public TotalMemoryUsedForUploadPerMonthModel()
        {
            Content = new List<TotalMemoryUsedForUploadPerMonthSingleModel>();
        }

        public TotalMemoryUsedForUploadPerMonthModel(List<TotalMemoryUsedForUploadPerMonthSingleModel> content)
        {
            Content = content;
        }
    }
}