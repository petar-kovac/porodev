using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase()
        {

        }

        public TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}
