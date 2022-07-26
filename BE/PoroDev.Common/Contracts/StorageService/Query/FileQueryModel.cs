using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace PoroDev.Common.Contracts.StorageService.Query
{
    public class FileQueryModel
    {
        public string Id { get; set; }

        public string Filename { get; set; }

        public DateTime UploadDateTime { get; set; }

        public ulong Length { get; set; }

        public string ContentType { get; set; }

        public string UserName { get; set; }

        public string UserLastname { get; set; }

        public bool IsExe { get; set; }

        public bool IsDeleted { get; set; }

        public FileQueryModel()
        {
        }

        public FileQueryModel(string id,
                              string filename,
                              DateTime uploadDateTime,
                              ulong length,
                              string contentType,
                              string userName,
                              string userLastname,
                              bool isExe,
                              bool isDeleted)
        {
            Id = id;
            Filename = filename;
            UploadDateTime = uploadDateTime;
            Length = length;
            ContentType = contentType;
            UserName = userName;
            UserLastname = userLastname;
            IsExe = isExe;
            IsDeleted = isDeleted;
        }

        public FileQueryModel(GridFSFileInfo<ObjectId>? doc, string userName, string userLastname, bool isExe, bool isDeleted)
        {
            Id = doc.Id.ToString();
            Filename = doc.Filename;
            UploadDateTime = doc.UploadDateTime;
            Length = (ulong)doc.Length;
            ContentType = doc.Metadata.GetValue("ContentType").ToString();
            UserName = userName;
            UserLastname = userLastname;
            IsExe = isExe;
            IsDeleted = isDeleted;
        }
    }
}