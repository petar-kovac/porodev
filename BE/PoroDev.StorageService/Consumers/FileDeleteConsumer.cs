using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileDeleteConsumer : IConsumer<FileDeleteRequestGatewayToService>
    {
        private readonly IStorageService _storageService;
        public FileDeleteConsumer(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task Consume(ConsumeContext<FileDeleteRequestGatewayToService> context)
        {
            var modelToReturn = await _storageService.DeleteFile(new FileDeleteRequestServiceToDatabase()
            {
                FileId = context.Message.FileId
            });

            await context.RespondAsync<CommunicationModel<FileDeleteMessage>>(modelToReturn);
        }
    }
}
