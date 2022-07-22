using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalMemoryUsedForUploadPerMonthConsumer : IConsumer<TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalMemoryUsedForUploadPerMonthConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase> context)
        {
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);
            string currentMonth = GetMonthName();

            if(user.Role == 0)
            {
                TotalMemoryUsedForUploadPerMonthModel returnModel = new TotalMemoryUsedForUploadPerMonthModel();
                returnModel.TotalMemoryUsedForUploadInMBs = await CountTotalMemoryUsedForUploadPerMonth();
                returnModel.Month = currentMonth;

                var response = new CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>()
                {
                    Entity = returnModel,
                    ExceptionName = null,
                    HumanReadableMessage = null
                };

                await context.RespondAsync<CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>>(response);
            }
            else
            {
                string exceptionType = nameof(UserIsNotAdminException);
                string humanReadableMessage = "User must be admin!";

                var resposneException = new CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                await context.RespondAsync(resposneException);
            }
        }

        public async Task<double> CountTotalMemoryUsedForUploadPerMonth()
        {
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();
            ulong countTotalMemoryUsedForDownloadPerMonthInKBs = 0;

            foreach (var user in allUsers)
            {
                countTotalMemoryUsedForDownloadPerMonthInKBs += user.FileUploadTotal;
            }

            double countTotalMemoryUsedForDownloadPerMonthInMBs = 0;
            countTotalMemoryUsedForDownloadPerMonthInMBs = Convert.ToDouble(countTotalMemoryUsedForDownloadPerMonthInKBs / 1024.0);

            return countTotalMemoryUsedForDownloadPerMonthInMBs;
        }

        public string GetMonthName()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("MMMM");
        }
    }
}
