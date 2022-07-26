using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
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
            var responseModel = await TotalNumberOfDeletedFiles(context.Message);

            await context.RespondAsync<CommunicationModel<TotalNumberOfDeletedFilesModel>>(responseModel);
        }

        private async Task<CommunicationModel<TotalNumberOfDeletedFilesModel>> TotalNumberOfDeletedFiles(TotalNumberOFDeletedFilesRequestServiceToDatabase totalNumberOfDeletedFiles)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalNumberOfDeletedFiles.UserId);

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalNumberOfDeletedFilesModel>(new UserIsNotAdminException());
            }

            TotalNumberOfDeletedFilesModel returnModel = new TotalNumberOfDeletedFilesModel();
            returnModel.NumberOfDeletedFiles = await CountNumberOfDeletedFiles();

            var responseTotalNumberOfDeletedFiles = new CommunicationModel<TotalNumberOfDeletedFilesModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return responseTotalNumberOfDeletedFiles;
        }

        private async Task<int> CountNumberOfDeletedFiles()
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