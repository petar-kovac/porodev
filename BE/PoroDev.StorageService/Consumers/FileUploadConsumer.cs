using MassTransit;
using PoroDev.Common;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileUploadConsumer : IConsumer<IUploadRequest>
    {
        private readonly IStorageService _storageService;

        public FileUploadConsumer(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task Consume(ConsumeContext<IUploadRequest> context)
        {

            var modelToReturn = await _storageService.UploadFile(new FileUploadRequestServiceToDatabase()
            {
                File = context.Message.File,
                FileName = context.Message.FileName,
                ContentType = context.Message.ContentType,
                UserId = context.Message.UserId
            });

            await context.RespondAsync(modelToReturn);
        }
    }
}