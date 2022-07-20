using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.BillingReport.TotalDownload
{
    public class TotalDownloadResponse
    {
        public double DownloadSize { get; set; }

        public TotalDownloadResponse()
        {
        }

        public TotalDownloadResponse(double downloadSize)
        {
            DownloadSize = downloadSize;
        }
    }
}
