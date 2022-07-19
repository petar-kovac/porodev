namespace PoroDev.Common.Contracts.BillingReport.TotalUpload
{
    public class TotalUploadResponse
    {
        public double UploadSize { get; set; }

        public TotalUploadResponse()
        {
        }

        public TotalUploadResponse(double uploadSize)
        {
            UploadSize = uploadSize;
        }
    }
}