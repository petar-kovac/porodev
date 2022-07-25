using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers
{
    public class TotalNumberOfUsersRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalNumberOfUsersRequestGatewayToService()
        {

        }

        public TotalNumberOfUsersRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}
