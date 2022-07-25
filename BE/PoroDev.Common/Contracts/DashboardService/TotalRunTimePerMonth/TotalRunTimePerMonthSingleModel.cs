using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthSingleModel
    {
        public string Month { get; set; }

        public int TotalRunTime { get; set; }

        public TotalRunTimePerMonthSingleModel()
        {

        }

        public TotalRunTimePerMonthSingleModel(string month, int totalRunTime )
        {
            Month = month;
            TotalRunTime = totalRunTime;
        }
    }
}
