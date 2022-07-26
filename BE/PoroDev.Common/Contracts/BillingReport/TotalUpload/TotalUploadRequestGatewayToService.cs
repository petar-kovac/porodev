namespace PoroDev.Common.Contracts.BillingReport.TotalUpload
{
    public class TotalUploadRequestGatewayToService
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public string Month { get; init; }

        public TotalUploadRequestGatewayToService()
        {
        }

        public TotalUploadRequestGatewayToService(Guid adminId, Guid userId, string month)
        {
            AdminId = adminId;
            UserId = userId;
            Month = month;
        }
    }
}