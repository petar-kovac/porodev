using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthModel
    {
        public string _month { get; set; }

        public double _totalMemoryUsedForDownloadInMBs { get; set; }
        
        public TotalMemoryUsedForDownloadPerMonthModel()
        {

        }

        public TotalMemoryUsedForDownloadPerMonthModel(double totalMemoryUsedForDownloadInMBs, string month)
        {
            _totalMemoryUsedForDownloadInMBs = totalMemoryUsedForDownloadInMBs;
            _month = month;
        }
    }
}
