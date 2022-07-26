namespace PoroDev.Common.Contracts.BillingReport.TotalDownload
{
    public class TotalDownloadResponse
    {
        public double DownloadSize { get; set; }

        public double DownloadPrice { get; set; }

        public TotalDownloadResponse()
        {
        }

        public TotalDownloadResponse(double downloadSize, double downloadPrice)
        {
            DownloadSize = downloadSize;
            DownloadPrice = downloadPrice;
        }
    }
}