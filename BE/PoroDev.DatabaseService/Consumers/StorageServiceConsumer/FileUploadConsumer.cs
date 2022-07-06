using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileUploadConsumer : IConsumer<FileUploadRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IFileRepository _fileRepository;
  

        public FileUploadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task Consume(ConsumeContext<FileUploadRequestServiceToDatabase> context)
        {
                   
            ObjectId id = await _fileRepository.UploadFile(context.Message.FileName, context.Message.File, context.Message.ContentType, context.Message.UserId);

            FileUploadModel model = new(context.Message.FileName, context.Message.File, context.Message.ContentType, context.Message.UserId);
            var response = new CommunicationModel<FileUploadModel>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };
      
            string fileId = id.ToString();

            FileData createModel = new (fileId, context.Message.UserId, false);

            await _unitOfWork.UserFiles.CreateAsync(createModel);
            await _unitOfWork.SaveChanges();

            await context.RespondAsync(response);

        }
    }
}