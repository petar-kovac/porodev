namespace PoroDev.GatewayAPI.Models.StorageService
{
    public class FileUploadRequest
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string ContentType { get; set; }

        public Guid UserId { get; set; }

        public FileUploadRequest()
        {
        }

        public FileUploadRequest(IFormFile file, Guid userId)
        {
            File = ConvertFormFileToBytes(file);
            FileName = file.FileName;
            ContentType = file.ContentType;
            UserId = userId;

        }

        private byte[] ConvertFormFileToBytes(IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
