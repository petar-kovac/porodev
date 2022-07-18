namespace PoroDev.Common.Contracts.StorageService.DeleteFile
{
    public class FileDeleteRequestGatewayToService
    {
        public string FileId { get; init; }

        public FileDeleteRequestGatewayToService()
        {
        }

        public FileDeleteRequestGatewayToService(string fileId)
        {
            FileId = fileId;
        }
    }
}