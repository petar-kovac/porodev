namespace PoroDev.Common.Contracts.BillingReport.TotalUpload
{
    public class TotalUploadRequestGatewayToService
    {
        public Guid AdminId { get; init; }

        public Guid UserId { get; init; }

        public TotalUploadRequestGatewayToService()
        {
        }

        public TotalUploadRequestGatewayToService(Guid adminId, Guid userId)
        {
            AdminId = adminId;
            UserId = userId;
        }
    }
}