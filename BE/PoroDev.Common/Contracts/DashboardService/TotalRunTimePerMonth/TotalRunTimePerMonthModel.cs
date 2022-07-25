using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthModel
    {
        public List<TotalRunTimePerMonthSingleModel> Content { get; set; }

        public TotalRunTimePerMonthModel()
        {
            Content = new List<TotalRunTimePerMonthSingleModel>();
        }

        public TotalRunTimePerMonthModel(List<TotalRunTimePerMonthSingleModel> content)
        {
            Content = content;
        }
    }
}
