using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles
{
    public class TotalNumberOfUploadedFilesRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalNumberOfUploadedFilesRequestServiceToDatabase()
        {

        }
        public TotalNumberOfUploadedFilesRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}
