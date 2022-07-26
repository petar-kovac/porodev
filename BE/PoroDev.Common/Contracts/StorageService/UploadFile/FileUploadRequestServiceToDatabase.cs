using MassTransit;

namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public class FileUploadRequestServiceToDatabase
    {
        public string FileName { get; set; }
        public MessageData<byte[]> File { get; set; }
        public string ContentType { get; set; }

        public Guid UserId { get; set; }

        public FileUploadRequestServiceToDatabase()
        {
        }

        public FileUploadRequestServiceToDatabase(string fileName, MessageData<byte[]> file, string contentType, Guid userId)
        {
            FileName = fileName;
            File = file;
            ContentType = contentType;
            UserId = userId;
        }
    }
}