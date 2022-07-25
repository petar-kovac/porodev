namespace PoroDev.GatewayAPI.Models.StorageService
{
    public class FileDownloadResponse
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}