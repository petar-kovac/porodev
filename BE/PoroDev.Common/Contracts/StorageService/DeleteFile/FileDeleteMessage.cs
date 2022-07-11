namespace PoroDev.Common.Contracts.StorageService.DeleteFile
{
    public class FileDeleteMessage
    {
        public string FileId { get; init; }

        public FileDeleteMessage()
        {
        }

        public FileDeleteMessage(string fileId)
        {
            FileId = fileId;
        }
    }
}