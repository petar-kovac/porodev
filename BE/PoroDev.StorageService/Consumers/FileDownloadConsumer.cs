using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileDownloadConsumer : IConsumer<FileDownloadMsg>
    {
        private readonly IStorageService _storageService;

        public FileDownloadConsumer(IStorageService storageService)
        {
            _storageService = storageService;
        }
        public async Task Consume(ConsumeContext<FileDownloadMsg> context)
        {
            var modelToReturn = await _storageService.DownloadFile(context.Message);

            await context.RespondAsync<CommunicationModel<FileDownloadMsg>>(modelToReturn);
        }
    }
}
