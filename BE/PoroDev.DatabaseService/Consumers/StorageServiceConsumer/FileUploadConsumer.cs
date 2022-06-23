using AutoMapper;
using MassTransit;
using PoroDev.Common.Models.StorageModels;
using PoroDev.Common.Models.StorageModels.UploadFile;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.DatabaseService.Repositories.Contracts;
using System;

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
           
            
            await _storageRepository.UploadFile(context.Message.File, context.Message.FileName, context.Message.UserId);
            //we need response model here
            FileUploadModel model = new();
            model.File = context.Message.File;
            model.FileName = context.Message.FileName;
            model.UserId = context.Message.UserId;
            await context.RespondAsync(model);

            //await _storageRepository.UploadFile(stream, fileName, id);
        }
    }
}