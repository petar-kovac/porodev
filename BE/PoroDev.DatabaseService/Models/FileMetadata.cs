using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace PoroDev.DatabaseService.Models
{
    public class FileMetadata
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }

        public FileMetadata()
        {
        }

        public FileMetadata(string fileName, DateTime uploadTime, string fileId)
        {
            FileName = fileName;
            UploadTime = uploadTime;
            FileId = fileId;
        }

        public FileMetadata(GridFSFileInfo<ObjectId> fileInfo)
        {
            FileName = fileInfo.Filename;
            UploadTime = fileInfo.UploadDateTime;
            FileId = fileInfo.Id.ToString();
        }
    }
}