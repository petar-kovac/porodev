namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadRequestGatewayToService
    {
        public Guid FileId { get; init; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public FileDownloadRequestGatewayToService()
        {
        }

        public FileDownloadRequestGatewayToService(Guid fileId, string fileName, byte[] file)
        {
            FileId = fileId;
            FileName = fileName;
            File = file;
        }
    }
}