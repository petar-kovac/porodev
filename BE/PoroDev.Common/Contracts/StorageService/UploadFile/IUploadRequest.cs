using MassTransit;

namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public interface IUploadRequest
    {
        public string FileName { get; set; }

        public MessageData<byte[]> File { get; set; }

        public string ContentType { get; set; }

        public Guid UserId { get; set; }
    }
}