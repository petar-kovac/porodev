namespace PoroDev.Common.Contracts.StorageService.DeleteFile
{
    public class FileDeleteRequestServiceToDatabase
    {
        public string FileId { get; init; }

        public FileDeleteRequestServiceToDatabase()
        {
        }

        public FileDeleteRequestServiceToDatabase(string fileId)
        {
            FileId = fileId;
        }
    }
}