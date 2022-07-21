using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.UserReportsModels.Data
{
    public class UserReportsData
    {
        public ulong FileDownloadTotal { get; set; }

        public ulong FileUploadTotal { get; set; }

        public ushort RuntimeTotal { get; set; }

        public string Month { get; set; }

        public UserReportsData()
        {

        }

        public UserReportsData(ulong fileDownloadTotal, ulong fileUploadTotal, ushort runtimeTotal, string month)
        {
            FileDownloadTotal = fileDownloadTotal;
            FileUploadTotal = fileUploadTotal;
            RuntimeTotal = runtimeTotal;
            Month = month;
        }
    }
}
