namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadRequestGatewayToService
    {
        public string FileId { get; init; }
        
        public Guid UserId { get; set; }

        public FileDownloadRequestGatewayToService()
        {
        }

        public FileDownloadRequestGatewayToService(string fileId, Guid userId)
        {
            FileId = fileId;
            UserId = userId;
        }
    }
}