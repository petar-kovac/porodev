using MassTransit;

namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadMessage
    {
        public MessageData<byte[]> File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public FileDownloadMessage()
        {
        }
    }
}