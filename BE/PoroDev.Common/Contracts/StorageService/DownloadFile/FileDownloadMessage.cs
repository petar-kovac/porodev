namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadMessage
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public string EncryptedKey { get; set; }

        public string EncryptedIv { get; set; }

        public FileDownloadMessage()
        {
        }

        public FileDownloadMessage(byte[] file, string fileName, string contentType, string encryptedKey, string encryptedIv)
        {
            File = file;
            FileName = fileName;
            ContentType = contentType;
            EncryptedKey = encryptedKey;
            EncryptedIv = encryptedIv;
        }
    }
}