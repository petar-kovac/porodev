using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public int NumberOfMonthsToShow { get; set; }

        public TotalMemoryUsedForDownloadPerMonthRequestGatewayToService()
        {

        }
        public TotalMemoryUsedForDownloadPerMonthRequestGatewayToService(Guid userId, int numberOfMonthsToShow)
        {
            UserId = userId;
            NumberOfMonthsToShow = numberOfMonthsToShow;
        }
    }
}
