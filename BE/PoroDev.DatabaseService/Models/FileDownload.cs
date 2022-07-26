namespace PoroDev.DatabaseService.Models
{
    public class FileDownload
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}