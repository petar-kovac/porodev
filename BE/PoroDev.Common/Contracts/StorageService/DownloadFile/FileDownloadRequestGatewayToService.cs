namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadRequestGatewayToService
    {
        public string FileId { get; init; }

        public Guid UserId { get; set; }

        public string PublicKey { get; set; }

        public FileDownloadRequestGatewayToService()
        {
        }

        public FileDownloadRequestGatewayToService(string fileId, Guid userId, string publicKey)
        {
            FileId = fileId;
            UserId = userId;
            PublicKey = publicKey;
        }
    }
}