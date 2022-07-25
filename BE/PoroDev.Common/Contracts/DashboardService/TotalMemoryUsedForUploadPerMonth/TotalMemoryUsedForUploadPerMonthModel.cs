using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
