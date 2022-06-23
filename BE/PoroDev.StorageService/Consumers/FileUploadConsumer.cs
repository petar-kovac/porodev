using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    //IConsumer<ExecuteProjectRequestGatewayToService>

    public class FileUploadConsumer : IConsumer<FileUploadRequestGatewayToService>
    {   
        private readonly IStorageService _storageService;

        public FileUploadConsumer(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task Consume(ConsumeContext<FileUploadRequestGatewayToService> context)
        {
            var modelToReturn = await _storageService.UploadFile(context.Message);

            await context.RespondAsync<CommunicationModel<FileUploadModel>>(modelToReturn);
        }
    }
}
