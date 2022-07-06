namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadMessage
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public FileDownloadMessage()
        {
        }

        public FileDownloadMessage(byte[] file, string fileName, string contentType)
        {
            File = file;
            FileName = fileName;
            ContentType = contentType;
        }
    }
}