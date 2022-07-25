using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public int NumberOfMonthsToShow { get; set; }

        public TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase()
        {

        }
        public TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase(Guid userId, int numberOfMonthsToShow)
        {
            UserId = userId;
            NumberOfMonthsToShow = numberOfMonthsToShow;
        }
    }
}
