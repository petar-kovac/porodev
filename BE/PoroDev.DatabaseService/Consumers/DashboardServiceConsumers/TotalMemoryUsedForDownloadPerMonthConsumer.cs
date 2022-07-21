using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Helpers;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalMemoryUsedForDownloadPerMonthConsumer : IConsumer<TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalMemoryUsedForDownloadPerMonthConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase> context)
        {
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);
            string currentMonth = CurrentMonth.GetMonthName();

            if (user.Role == 0)
            {
                TotalMemoryUsedForDownloadPerMonthModel returnModel = new TotalMemoryUsedForDownloadPerMonthModel();
                returnModel.TotalMemoryUsedForDownloadInMBs = await CountTotalMemoryUsedForDownloadPerMonth();
                returnModel.Month = currentMonth;

                var response = new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>()
                {
                    Entity = returnModel,
                    ExceptionName = null,
                    HumanReadableMessage = null
                };

                await context.RespondAsync<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>>(response);
            }
            else
            {
                string exceptionType = nameof(UserIsNotAdminException);
                string humanReadableMessage = "User must be admin!";

                var resposneException = new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                await context.RespondAsync(resposneException);
            }

            //var responseModel = await TotalMemoryUsedForDownloadPerMonth(context.Message);

        }

        //private async Task<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>> TotalMemoryUsedForDownloadPerMonth
        //    (TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase totalMemoryUsedForDownloadPerMonthModel)
        //{
        //    DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalMemoryUsedForDownloadPerMonthModel.UserId);
        //    string currentMonth = CurrentMonth.GetMonthName();

        //    if (admin.Role != 0)
        //    {
        //        return new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>(new UserIsNotAdminException());
        //    }

        //    TotalMemoryUsedForDownloadPerMonthModel returnModel = new TotalMemoryUsedForDownloadPerMonthModel();
        //    returnModel.TotalMemoryUsedForDownloadInMBs = await CountTotalMemoryUsedForDownloadPerMonth();
        //    returnModel.Month = currentMonth;

        //    var responseTotalMemoryUsedForDownloadPerMonth = new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>()
        //    {
        //        Entity = returnModel,
        //        ExceptionName = null,
        //        HumanReadableMessage = null
        //    };

        //    return responseTotalMemoryUsedForDownloadPerMonth;
        //}

        private async Task<double> CountTotalMemoryUsedForDownloadPerMonth()
        {
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();
            ulong countTotalMemoryUsedForDownloadPerMonthInKBs = 0;

            foreach(var user in allUsers)
            {
                countTotalMemoryUsedForDownloadPerMonthInKBs += user.FileDownloadTotal;
            }

            double countTotalMemoryUsedForDownloadPerMonthInMBs = 0;
            countTotalMemoryUsedForDownloadPerMonthInMBs = Convert.ToDouble(countTotalMemoryUsedForDownloadPerMonthInKBs / 1024.0);

            return countTotalMemoryUsedForDownloadPerMonthInMBs;
        }
    }
}
