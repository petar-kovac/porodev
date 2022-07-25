using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthSingleModel
    {
        public string Month { get; set; }
        public double TotalMemoryUsedForDownloadInMBs { get; set; }

        public TotalMemoryUsedForDownloadPerMonthSingleModel()
        {

        }
        public TotalMemoryUsedForDownloadPerMonthSingleModel(string month, double totalMemoryUsedForDownloadInMBs)
        {
            Month = month;
            TotalMemoryUsedForDownloadInMBs = totalMemoryUsedForDownloadInMBs;
        }
    }
}
