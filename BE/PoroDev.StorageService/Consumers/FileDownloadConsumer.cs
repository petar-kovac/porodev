using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileDownloadConsumer
    {
        private readonly IStorageService _storageService;

        public FileDownloadConsumer(IStorageService storageService)
        {
            _storageService = storageService;
        }
    }
}
