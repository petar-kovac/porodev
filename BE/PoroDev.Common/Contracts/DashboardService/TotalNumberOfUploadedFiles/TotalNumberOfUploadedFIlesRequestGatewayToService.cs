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