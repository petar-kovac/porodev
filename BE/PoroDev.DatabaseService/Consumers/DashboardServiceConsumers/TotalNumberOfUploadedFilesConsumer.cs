using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
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
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);

            if (user.Role == 0)
            {
                TotalNumberOfUploadedFilesModel returnModel = new TotalNumberOfUploadedFilesModel();
                returnModel.NumberOfUploadedFiles = await CountNumberOfUploadedFiles();

                var response = new CommunicationModel<TotalNumberOfUploadedFilesModel>()
                {
                    Entity = returnModel,
                    ExceptionName = null,
                    HumanReadableMessage = null
                };

                await context.RespondAsync<CommunicationModel<TotalNumberOfUploadedFilesModel>>(response);
            }
            else
            {
                string exceptionType = nameof(UserIsNotAdminException);
                string humanReadableMessage = "User must be admin!";

                var resposneException = new CommunicationModel<TotalNumberOfUploadedFilesModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                await context.RespondAsync(resposneException);
            }
        }

        public async Task<int> CountNumberOfUploadedFiles()
        {
            List<FileData> userFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();
            int countTotalNumberOfUploadedFiles = 0;

            countTotalNumberOfUploadedFiles = userFiles.Count;

            return countTotalNumberOfUploadedFiles;
        }
    }
}