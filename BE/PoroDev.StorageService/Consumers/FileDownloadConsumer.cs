using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileDownloadConsumer : IConsumer<FileDownloadRequestGatewayToService>
    {
        private readonly IStorageService _storageService;

        public FileDownloadConsumer(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task Consume(ConsumeContext<FileDownloadRequestGatewayToService> context)
        {
            var modelToReturn = await _storageService.DownloadFile(new FileDownloadRequestServiceToDatabase()
            {
                FileId = context.Message.FileId,
                UserId = context.Message.UserId
            });

            await context.RespondAsync<CommunicationModel<FileDownloadMessage>>(modelToReturn);
        }
    }
}