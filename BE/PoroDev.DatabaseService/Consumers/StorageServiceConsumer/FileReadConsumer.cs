using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.StorageServiceConsumer
{
    public class FileReadConsumer : IConsumer<FileReadModel>
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

        public async Task Consume(ConsumeContext<FileReadModel> context)
        {
            List<FileData> allUserFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();
            FileReadModel returnModel = new FileReadModel();
    
            for(int i = 0; i < allUserFiles.Count; i++)
            {
                var fileModel = allUserFiles[i];
                var fileReadSingleModel = await _fileRepository.ReadFiles(fileModel.FileId);
                returnModel.FileNames.Add(fileReadSingleModel.FileName);
                returnModel.UploadTime.Add(fileReadSingleModel.UploadTime);
            }

            var response = new CommunicationModel<FileReadModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            await context.RespondAsync<CommunicationModel<FileReadModel>>(response);
        }
    }
}
