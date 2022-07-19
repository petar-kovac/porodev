using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthModel
    {
        public string _month { get; set; }

        public double _totalMemoryUsedForUploadInMBs { get; set; }

        public TotalMemoryUsedForUploadPerMonthModel()
        {

        }

        public TotalMemoryUsedForUploadPerMonthModel(string month, double totalMemoryUsedForUploadInMBs)
        {
            _month = month;
            _totalMemoryUsedForUploadInMBs = totalMemoryUsedForUploadInMBs;
        }
    }
}
