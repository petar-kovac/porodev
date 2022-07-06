using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileReadConsumer : IConsumer<FileReadRequestGatewayToService>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IFileRepository _fileRepository;

        public FileReadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task Consume(ConsumeContext<FileReadRequestGatewayToService> context)
        {
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);
            FileReadModel returnModel = new FileReadModel();

            //super admin = 0; normal user = 1 
            if (user.Role == 0)
            {
                returnModel = await findAdminFiles();
            }
            else
            {
                returnModel = await findUserFiles(user.Id);
            }

     /*       List<FileData> allUserFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();
          
    
            for(int i = 0; i < allUserFiles.Count; i++)
            {
                var fileModel = allUserFiles[i];
                var fileReadSingleModel = await _fileRepository.ReadFiles(fileModel.FileId);
                returnModel.FileNames.Add(fileReadSingleModel.FileName);
                returnModel.UploadTime.Add(fileReadSingleModel.UploadTime);
            } */

            var response = new CommunicationModel<FileReadModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            await context.RespondAsync<CommunicationModel<FileReadModel>>(response);
        }

        public async Task<FileReadModel> findAdminFiles()
        {
            List<FileData> allAdminFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();

            FileReadModel returnModel = new FileReadModel();

            for (int i = 0; i < allAdminFiles.Count; i++)
            {
                var fileModel = allAdminFiles[i];
                var fileReadSingleModel = await _fileRepository.ReadFiles(fileModel.FileId);
                returnModel.Content.Add(fileReadSingleModel);
            }

            return returnModel;
        }

        public async Task<FileReadModel> findUserFiles(Guid userId)
        {
            List<FileData> allUserFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Id.Equals(userId))).ToList<FileData>();

            FileReadModel returnModel = new FileReadModel();

            for(int i = 0; i < allUserFiles.Count; i++)
            {
                if (allUserFiles[i].IsDeleted == false)
                {
                    var fileModel = allUserFiles[i];
                    var fileReadSingleModel = await _fileRepository.ReadFiles(fileModel.FileId);
                    returnModel.Content.Add(fileReadSingleModel);
                }
            }
            return returnModel;
        }
    }
}
