namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadSingleModel
    {
        public string FileName { get; set; }

        public DateTime UploadTime { get; set; }

        public FileReadSingleModel()
        {
        }

        public FileReadSingleModel(string fileName, DateTime uploadTime)
        {
            FileName = fileName;
            UploadTime = uploadTime;
        }
    }
}