using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using System.Security.Cryptography;
using static PoroDev.DatabaseService.Constants.Constants;

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
            byte[] fileForUpload = context.Message.File;

            byte[] encryptedFile;
            byte[] key;
            byte[] iv;

            using(Aes cipher = Aes.Create())
            {
                cipher.Padding = PaddingMode.ISO10126;

                ICryptoTransform encryptor = cipher.CreateEncryptor(cipher.Key, cipher.IV);

                var cipherText = encryptor.TransformFinalBlock(fileForUpload, 0, fileForUpload.Length);
                
                key = cipher.Key;
                encryptedFile = cipherText;
                iv = cipher.IV;
            }


            ObjectId id = await _fileRepository.UploadFile(context.Message.FileName, encryptedFile, context.Message.ContentType, context.Message.UserId);

            var model = new FileUploadModel(context.Message.FileName, context.Message.File, context.Message.ContentType, context.Message.UserId);
            var response = new CommunicationModel<FileUploadModel>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

            string fileId = id.ToString();

            using(Aes cipher = Aes.Create())
            {
                cipher.Padding = PaddingMode.ISO10126;
                cipher.Key = secretKey;
                cipher.IV = secretIv;

                ICryptoTransform encryptor = cipher.CreateEncryptor(secretKey, secretIv);

                key = encryptor.TransformFinalBlock(key, 0, key.Length);
                iv = encryptor.TransformFinalBlock(iv, 0, iv.Length);
            }

            var createModel = new FileData(fileId, context.Message.UserId, false, key, iv);

            await _unitOfWork.UserFiles.CreateAsync(createModel);
            await _unitOfWork.SaveChanges();

            await context.RespondAsync(response);
        }
    }
}