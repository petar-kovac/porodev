namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadMessage
    {
        public byte[] File { get; set; }

        public FileDownloadMessage()
        {
        }

        public FileDownloadMessage(byte[] file)
        {
            File = file;
        }
    }
}