using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles
{
    public class TotalNumberOfUploadedFilesRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalNumberOfUploadedFilesRequestGatewayToService()
        {

        }

        public TotalNumberOfUploadedFilesRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}
