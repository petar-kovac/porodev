using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers
{
    public class TotalRunTimeForAllUsersRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalRunTimeForAllUsersRequestServiceToDatabase()
        {

        }

        public TotalRunTimeForAllUsersRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}
