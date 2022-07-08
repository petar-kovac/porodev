namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public class FileUploadModel
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string ContentType { get; set; }

        public Guid UserId { get; set; }

        public FileUploadModel()
        {
        }

        public FileUploadModel(Guid userId)
        {
            UserId = userId;
        }

        public FileUploadModel(string fileName, byte[] file, string contentType, Guid userId)
        {
            FileName = fileName;
            File = file;
            ContentType = contentType;
            UserId = userId;
        }
    }
}