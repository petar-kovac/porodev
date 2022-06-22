using AutoMapper;
using MassTransit;
using PoroDev.Common.Models.StorageModels;
using PoroDev.Common.Models.StorageModels.UploadFile;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileUploadConsumer : IConsumer<FileUploadModel>
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IStorageRepository _storageRepository;
        /*using Stream stream = uploadModel.File.OpenReadStream();
            string fileName = uploadModel.File.FileName;
            Guid id = uploadModel.UserId;*/

        public FileUploadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)  
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task Consume(ConsumeContext<FileUploadModel> context)
        {
            //using Stream stream = context.Message.File.OpenReadStream();
            //string fileName = context.Message.File.FileName;
            //Guid id = context.Message.UserId;

            //await _storageRepository.UploadFile(stream, fileName, id);
        }
    }
}