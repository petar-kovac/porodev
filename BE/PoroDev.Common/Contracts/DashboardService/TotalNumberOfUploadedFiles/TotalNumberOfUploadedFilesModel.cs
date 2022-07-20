using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles
{
    public class TotalNumberOfUploadedFilesModel
    {
        public int NumberOfUploadedFiles { get; set; }

        public TotalNumberOfUploadedFilesModel()
        {

        }
        public TotalNumberOfUploadedFilesModel(int numberOfUploadedFiles)
        {
            NumberOfUploadedFiles = numberOfUploadedFiles;
        }
    }
}
