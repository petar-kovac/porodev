using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileUploadConsumer : IConsumer<FileUploadRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IFileRepository _fileRepository;
        /*using Stream stream = uploadModel.File.OpenReadStream();
            string fileName = uploadModel.File.FileName;
            Guid id = uploadModel.UserId;*/

        public FileUploadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task Consume(ConsumeContext<FileUploadRequestServiceToDatabase> context)
        {
            //using Stream stream = context.Message.File.OpenReadStream();
            //string fileName = context.Message.File.FileName;
            //Guid id = context.Message.UserId;

            ObjectId id = await _fileRepository.UploadFile(context.Message.FileName, context.Message.File, context.Message.UserId);
            //we need response model here
            FileUploadModel model = new(context.Message.FileName, context.Message.File, context.Message.UserId);
            var response = new CommunicationModel<FileUploadModel>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };
            await context.RespondAsync(response);

            string fileId = id.ToString();

            //we need to map here user with Id from parametar of method (userId);
            DataUserModel userModel = new();
            userModel.Id = context.Message.UserId;

            FileData createModel = new FileData(fileId, userModel);

            await _unitOfWork.UserFiles.CreateAsync(createModel);
            await _unitOfWork.SaveChanges();

            //await _storageRepository.UploadFile(stream, fileName, id);
        }
    }
}