namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public class FileUploadResponse
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public Guid UserId { get; set; }
    }
}
