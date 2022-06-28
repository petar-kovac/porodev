namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public class FileUploadModel
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public Guid UserId { get; set; }

        public FileUploadModel()
        {
        }

        public FileUploadModel(string fileName, byte[] file, Guid userId)
        {
            FileName = fileName;
            File = file;
            UserId = userId;
        }
    }
}