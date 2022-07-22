namespace PoroDev.Common.Contracts.StorageService.Query
{
    public class SingleFileQueryServiceToDatabase
    {
        public Guid UserId { get; set; }

        public string? FileId { get; set; }

        public string? FileName { get; set; }

        public DateTime? UploadTime { get; set; }

        public string? UserName { get; set; }

        public string? UserLastName { get; set; }

        public ulong? Size { get; set; }

        public string? ContentType { get; set; }

        public SingleFileQueryServiceToDatabase()
        {
        }

        public SingleFileQueryServiceToDatabase(Guid userId, string? fileId = null, string? fileName = null, DateTime? uploadTime = null, string? userName = null, string? userLastName = null, ulong? size = null, string? contentType = null)
        {
            UserId = userId;
            FileId = fileId;
            FileName = fileName;
            UploadTime = uploadTime;
            UserName = userName;
            UserLastName = userLastName;
            Size = size;
            ContentType = contentType;
        }
    }
}