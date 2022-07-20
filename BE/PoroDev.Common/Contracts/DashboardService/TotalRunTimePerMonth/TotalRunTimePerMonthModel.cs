using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthModel
    {
        public string _month { get; set; }

        public ulong _totalRunTime { get; set; }

        public TotalRunTimePerMonthModel()
        {

        }

        public TotalRunTimePerMonthModel(string month, ushort totalRunTime)
        {
            _month = month;
            _totalRunTime = totalRunTime;
        }
    }
}
