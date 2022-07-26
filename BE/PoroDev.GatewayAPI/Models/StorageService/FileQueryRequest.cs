namespace PoroDev.GatewayAPI.Models.StorageService
{
    public class FileQueryRequest
    {
        public string? FileId { get; set; }

        public string? FileName { get; set; }

        public DateTime? UploadTime { get; set; }

        public ulong? Size { get; set; }

        public string? ContentType { get; set; }

        public FileQueryRequest()
        {
        }

        public FileQueryRequest(string? fileId = null, string? fileName = null, DateTime? uploadTime = null, ulong? size = null, string? contentType = null)
        {
            FileId = fileId;
            FileName = fileName;
            UploadTime = uploadTime;
            Size = size;
            ContentType = contentType;
        }
    }
}