using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public int NumberOfMonthsToShow { get; set; }

        public TotalRunTimePerMonthRequestServiceToDatabase()
        {

        }

        public TotalRunTimePerMonthRequestServiceToDatabase(Guid userId, int numberOfMonthsToShow)
        {
            UserId = userId;
            NumberOfMonthsToShow = numberOfMonthsToShow;
        }
    }
}
