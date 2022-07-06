namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadModel
    {
        public List<string> FileNames { get; set; }

        public List<DateTime> UploadTime { get; set; }

        public FileReadModel()
        {
            FileNames = new List<string>();
            UploadTime = new List<DateTime>();
        }

        public FileReadModel(List<string> fileNames, List<DateTime> uploadTime)
        {
            FileNames = fileNames;
            UploadTime = uploadTime;
        }
    }
}