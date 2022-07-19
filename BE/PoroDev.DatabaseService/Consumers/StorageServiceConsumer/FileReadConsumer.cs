using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileReadConsumer : BaseDbConsumer, IConsumer<FileReadRequestGatewayToService>
    {
        public FileReadConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<FileReadRequestGatewayToService> context)
        {
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);
            FileReadModel returnModel = new FileReadModel();

            //super admin = 0; normal user = 1
            if (user.Role == 0)
            {
                returnModel = await FindAdminFiles();
            }
            else
            {
                returnModel = await FindUserFiles(user.Id);
            }

            var response = new CommunicationModel<FileReadModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            await context.RespondAsync(response);
        }

        public async Task<FileReadModel> FindAdminFiles()
        {
            List<FileData> allAdminFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList();

            FileReadModel returnModel = new FileReadModel();

            foreach (FileData file in allAdminFiles)
            {
                DataUserModel user = await _unitOfWork.Users.GetByIdAsync(file.CurrentUserId);
                var fileReadSingleModel = await _fileRepository.ReadFiles(file.FileId, user.Name, user.Lastname);
                
                returnModel.Content.Add(fileReadSingleModel);
            }

            return returnModel;
        }

        public async Task<FileReadModel> FindUserFiles(Guid userId)
        {
            List<FileData> allUserFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Id.Equals(userId))).ToList();

            FileReadModel returnModel = new FileReadModel();

            foreach (FileData file in allUserFiles)
            {
                if (file.IsDeleted == false)
                {
                    var fileReadSingleModel = await _fileRepository.ReadFiles(file.FileId, file.CurrentUser.Name, file.CurrentUser.Lastname);
                    returnModel.Content.Add(fileReadSingleModel);
                }
            }

            return returnModel;
        }
    }
}