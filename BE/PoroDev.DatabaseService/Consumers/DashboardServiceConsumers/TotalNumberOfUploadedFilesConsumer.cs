using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalNumberOfUploadedFilesConsumer : IConsumer<TotalNumberOfUploadedFilesRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalNumberOfUploadedFilesConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalNumberOfUploadedFilesRequestServiceToDatabase> context)
        {
            TotalNumberOfUploadedFilesModel returnModel = new TotalNumberOfUploadedFilesModel();
            returnModel._numberOfUploadedFiles = await countNumberOfUploadedFiles(); 

            var response = new CommunicationModel<TotalNumberOfUploadedFilesModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            await context.RespondAsync<CommunicationModel<TotalNumberOfUploadedFilesModel>>(response);
        }

        
        public async Task<int> countNumberOfUploadedFiles()
        {
            List<FileData> userFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();
            int countTotalNumberOfUploadedFiles = 0;

            foreach (FileData file in userFiles)
            {
                if (file.IsDeleted == false)
                {
                    countTotalNumberOfUploadedFiles++;
                }
            }

            return countTotalNumberOfUploadedFiles;
        }
    }
}
