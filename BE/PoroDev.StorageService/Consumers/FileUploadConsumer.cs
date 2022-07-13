using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Consumers
{
    public class FileUploadConsumer : ConsumerBase, IConsumer<IUploadRequest>
    {

        public FileUploadConsumer(IStorageService storageService, IMapper mapper) : base(storageService, mapper)
        {
        }

        public async Task Consume(ConsumeContext<IUploadRequest> context)
        {
            var uploadRequest = context.Message;

            //may break
            var uploadRequestToDatabase = _mapper.Map<FileUploadRequestServiceToDatabase>(uploadRequest);

            var modelToReturn = await _storageService.UploadFile(uploadRequestToDatabase);

            await context.RespondAsync(modelToReturn);
        }
    }
}