using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileReadConsumer : IConsumer<FileReadRequestServiceToDatabase>
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

        public async Task Consume(ConsumeContext<FileReadRequestServiceToDatabase> context)
        {
            //List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();
            //Task<ICollection<TemplateEntity>?> FindAllAsync(Expression<Func<TemplateEntity, bool>> filter);
            //vidjeti da li ce pogoditi ovo kako treba

            List<FileData> allUserFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();
            FileReadModel returnModel = new FileReadModel();
    
         
            foreach(var fileModel in allUserFiles)
            {
                var fileReadSingleModel = await _fileRepository.ReadFiles(fileModel.FileId);
                returnModel.FileNames.Add(fileReadSingleModel.FileName);
                returnModel.UploadTime.Add(fileReadSingleModel.UploadTime);
            }

            await context.RespondAsync(returnModel);
        }
    }
}
