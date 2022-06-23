using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileUploadConsumer : IConsumer<FileUploadRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IStorageRepository _storageRepository;
        /*using Stream stream = uploadModel.File.OpenReadStream();
            string fileName = uploadModel.File.FileName;
            Guid id = uploadModel.UserId;*/

        public FileUploadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task Consume(ConsumeContext<FileUploadRequestServiceToDatabase> context)
        {
            //using Stream stream = context.Message.File.OpenReadStream();
            //string fileName = context.Message.File.FileName;
            //Guid id = context.Message.UserId;

            await _storageRepository.UploadFile(context.Message.FileName, context.Message.File, context.Message.UserId);
            //we need response model here
            FileUploadModel model = new(context.Message.FileName, context.Message.File, context.Message.UserId);
            var response = new CommunicationModel<FileUploadModel>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };
            await context.RespondAsync(response);

            //await _storageRepository.UploadFile(stream, fileName, id);
        }
    }
}