namespace PoroDev.GatewayAPI.Models.StorageService
{
    public class FileUploadResponse
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public Guid UserId { get; set; }
    }
}
