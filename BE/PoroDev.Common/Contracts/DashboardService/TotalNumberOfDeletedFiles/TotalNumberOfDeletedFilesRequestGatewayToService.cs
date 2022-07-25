using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles
{
    public class TotalNumberOfDeletedFilesRequestGatewayToService
    {
        public Guid UserId { get; set; }    

        public TotalNumberOfDeletedFilesRequestGatewayToService()
        {

        }
        public TotalNumberOfDeletedFilesRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}
