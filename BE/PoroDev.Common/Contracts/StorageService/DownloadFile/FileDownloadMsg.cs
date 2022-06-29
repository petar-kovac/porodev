namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadMsg
    {
        public string FileName { get; }

        public FileDownloadMsg()
        {
        }

        public FileDownloadMsg(string fileName)
        {
            FileName = fileName;
        }
    }
}