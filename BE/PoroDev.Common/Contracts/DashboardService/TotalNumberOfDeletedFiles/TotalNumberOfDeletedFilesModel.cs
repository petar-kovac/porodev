using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles
{
    public class TotalNumberOfDeletedFilesModel
    {
        public int _numberOfDeletedFiles { get; set; }

        public TotalNumberOfDeletedFilesModel()
        {

        }
        public TotalNumberOfDeletedFilesModel(int numberOfDeletedFiles)
        {
            _numberOfDeletedFiles = numberOfDeletedFiles;
        }
    }
}
