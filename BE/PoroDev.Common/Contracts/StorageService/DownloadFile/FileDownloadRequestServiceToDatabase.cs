namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadRequestServiceToDatabase
    {
        public Guid FileId { get; init; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public FileDownloadRequestServiceToDatabase()
        {
        }

        public FileDownloadRequestServiceToDatabase(Guid fileId, string fileName, byte[] file)
        {
            FileId = fileId;
            FileName = fileName;
            File = file;
        }
    }
}