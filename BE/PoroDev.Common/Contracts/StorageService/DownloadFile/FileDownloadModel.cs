namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadModel
    {
        public Guid FileId { get; init; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public FileDownloadModel()
        {
        }

        public FileDownloadModel(Guid fileId, string fileName, byte[] file)
        {
            FileId = fileId;
            FileName = fileName;
            File = file;
        }
    }
}