namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles
{
    public class TotalNumberOfDeletedFilesRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public TotalNumberOfDeletedFilesRequestGatewayToService()
        {
        }

        public TotalNumberOfDeletedFilesRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}