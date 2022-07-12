using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
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
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);
            FileReadModel readUserFiles = await findAllUserFiles(user.Id);
            List<FileData> allUserFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Id.Equals(context.Message.UserId))).ToList<FileData>();

            int counter = 0;

            bool flagNoFileWithThatName = false;
          
            bool flagFileWithThatNameExist = false;

            foreach (FileReadSingleModel file in readUserFiles.Content)
            {
                //3 slucaja -> 1 nema fajla sa tim imenom; 2 ima ali je obrisan; 3 ima i nije obrisan 2 SLUCAJ obrisan
                if(file.FileName == context.Message.FileName && allUserFiles[counter].IsDeleted == false)
                {
                    flagFileWithThatNameExist = true;

                    string exceptionType = nameof(FileUploadExistException);
                    string humanReadableMessage = "File already exists!";

                    var resposneException = new CommunicationModel<FileUploadModel>()
                    {
                        Entity = null,
                        ExceptionName = exceptionType,
                        HumanReadableMessage = humanReadableMessage
                    };

                    await context.RespondAsync(resposneException);
                }

                counter++;
            }

            if(flagFileWithThatNameExist == false)
            {
                ObjectId id = await _fileRepository.UploadFile(context.Message.FileName, context.Message.File, context.Message.ContentType, context.Message.UserId);

                var model = new FileUploadModel(context.Message.FileName, context.Message.File, context.Message.ContentType, context.Message.UserId);
                var response = new CommunicationModel<FileUploadModel>() { Entity = model, ExceptionName = null, HumanReadableMessage = null };

                string fileId = id.ToString();

                var createModel = new FileData(fileId, context.Message.UserId, false);

                await _unitOfWork.UserFiles.CreateAsync(createModel);
                await _unitOfWork.SaveChanges();

                await context.RespondAsync(response);
            }
        }

        public async Task<FileReadModel> findAllUserFiles(Guid userId)
        {
            List<FileData> allUserFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Id.Equals(userId))).ToList<FileData>();

            FileReadModel returnModel = new FileReadModel();

            foreach (FileData file in allUserFiles)
            {
               var fileReadSingleModel = await _fileRepository.ReadFiles(file.FileId);
               returnModel.Content.Add(fileReadSingleModel);
            }

            return returnModel;
        }
    }
}