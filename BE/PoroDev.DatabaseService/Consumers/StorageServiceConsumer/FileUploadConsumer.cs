using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using PoroDev.DatabaseService.Services.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileUploadConsumer : BaseDbConsumer, IConsumer<FileUploadRequestServiceToDatabase>
    {
        private readonly IEncryptionService _encryptionService;

        public FileUploadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository, IEncryptionService encryptionService) : base(unitOfWork, mapper, fileRepository)
        {
            _encryptionService = encryptionService;
        }

        public async Task Consume(ConsumeContext<FileUploadRequestServiceToDatabase> context)
        {
            var fileUploadResult = await UploadFile(context.Message);

            await context.RespondAsync(fileUploadResult);
        }

        private async Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestServiceToDatabase uploadRequest)
        {
            var userFileIds = (await _unitOfWork.UserFiles.FindAllAsync(userFile => userFile.CurrentUserId.Equals(uploadRequest.UserId)
                                                                                 && userFile.IsDeleted == false)).Select(a => a.FileId);
            foreach (var userFileId in userFileIds)
            {
                var fileData = await _fileRepository.ReadFileById(userFileId);

                if (fileData.FileName.Equals(uploadRequest.FileName))
                    return new CommunicationModel<FileUploadModel>(new FileUploadExistException("File already exists"));
            }

            try
            {
                var file = await uploadRequest.File.Value;

                file = _encryptionService.EncryptBytes(file);

                ObjectId fileId = await _fileRepository.UploadFile(uploadRequest.FileName,
                                                                       file,
                                                                       uploadRequest.ContentType);

                var fileUploadModel = new FileData(fileId.ToString(), uploadRequest.UserId, false, false);

                await _unitOfWork.UserFiles.CreateAsync(fileUploadModel);

                var userModel = await _unitOfWork.Users.GetByIdAsync(uploadRequest.UserId);
                userModel.FileUploadTotal += (ulong)file.LongLength;

                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new CommunicationModel<FileUploadModel>(new FileUploadException("Exception happened while upload file to db"));
            }

            var model = new FileUploadModel(uploadRequest.FileName, uploadRequest.ContentType, uploadRequest.UserId);

            var response = new CommunicationModel<FileUploadModel>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            return response;
        }
    }
}