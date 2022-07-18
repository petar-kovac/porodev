using MongoDB.Driver.GridFS;

namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadSingleModel
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }

        public FileReadSingleModel()
        {
        }

        public FileReadSingleModel(string fileName, DateTime uploadTime, string userName, string userLastName)
        {
            FileName = fileName;
            UploadTime = uploadTime;
            UserName = userName;
            UserLastName = userLastName;
        }
    }
}