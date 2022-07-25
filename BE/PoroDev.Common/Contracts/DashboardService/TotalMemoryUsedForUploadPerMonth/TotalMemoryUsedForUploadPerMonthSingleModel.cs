using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthSingleModel
    {
        public string Month { get; set; }

        public double TotalMemoryUsedForUploadInMBs { get; set; }

        public TotalMemoryUsedForUploadPerMonthSingleModel()
        {

        }

        public TotalMemoryUsedForUploadPerMonthSingleModel(string month, double totalMemoryUsedForUploadInMBs)
        {
            Month = month;
            TotalMemoryUsedForUploadInMBs = totalMemoryUsedForUploadInMBs;
        }

    }
}
