namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadRequestServiceToDatabase
    {
        public string FileId { get; init; }

        public Guid UserId { get; set; }

        public string PublicKey { get; set; }

        public FileDownloadRequestServiceToDatabase()
        {
        }

        public FileDownloadRequestServiceToDatabase(string fileId, Guid userId, string publicKey)
        {
            FileId = fileId;
            UserId = userId;
            PublicKey = publicKey;
        }
    }
}