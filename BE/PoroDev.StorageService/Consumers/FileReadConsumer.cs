using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileReadConsumer : IConsumer<FileReadRequestGatewayToService>
    {
        private readonly IStorageService _storageService;

        public FileReadConsumer(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task Consume(ConsumeContext<FileReadRequestGatewayToService> context)
        {
            var modelToReturn = await _storageService.ReadFiles(new FileReadRequestServiceToDatabase()
            {
                UserId = context.Message.UserId
            });

            await context.RespondAsync<CommunicationModel<FileReadModel>>(modelToReturn);
        }
    }
}
