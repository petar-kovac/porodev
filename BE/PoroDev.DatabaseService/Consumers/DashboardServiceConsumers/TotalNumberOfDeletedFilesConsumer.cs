using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalNumberOfDeletedFilesConsumer : IConsumer<TotalNumberOFDeletedFilesRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalNumberOfDeletedFilesConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalNumberOFDeletedFilesRequestServiceToDatabase> context)
        {
            TotalNumberOfDeletedFilesModel returnModel = new TotalNumberOfDeletedFilesModel();
            returnModel._numberOfDeletedFiles = await countNumberOfUploadedFiles();

            var response = new CommunicationModel<TotalNumberOfDeletedFilesModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            await context.RespondAsync<CommunicationModel<TotalNumberOfDeletedFilesModel>>(response);
        }

        public async Task<int> countNumberOfUploadedFiles()
        {
            List<FileData> userFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();
            int countTotalNumberOfDeletedFiles = 0;

            foreach (FileData file in userFiles)
            {
                if (file.IsDeleted == true)
                {
                    countTotalNumberOfDeletedFiles++;
                }
            }

            return countTotalNumberOfDeletedFiles;
        }
    }
}
