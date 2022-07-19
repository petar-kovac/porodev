using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers
{
    public class TotalRunTimeForAllUsersRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalRunTimeForAllUsersRequestGatewayToService()
        {

        }

        public TotalRunTimeForAllUsersRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}
