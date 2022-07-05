using Microsoft.AspNetCore.Http;

namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public class FileUploadRequestGatewayToService
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string ContentType { get; set; }

        public Guid UserId { get; set; }

        public FileUploadRequestGatewayToService()
        {
        }

        public FileUploadRequestGatewayToService(IFormFile file, Guid userId)
        {
            using Stream stream = file.OpenReadStream();
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            File = memoryStream.ToArray();
            FileName = file.FileName;
            ContentType = file.ContentType;

            // var content = file.OpenReadStream().
            //File = file;//izmjeniti na end point-u
            UserId = userId;
        }
    }
}