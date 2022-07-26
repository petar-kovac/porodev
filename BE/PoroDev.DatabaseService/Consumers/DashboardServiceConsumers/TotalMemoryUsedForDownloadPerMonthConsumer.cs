using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;
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
            var responseModel = await TotalMemoryUsedForDownloadPerMonth(context.Message);

            await context.RespondAsync<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>>(responseModel);

        }

        private async Task<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>> TotalMemoryUsedForDownloadPerMonth(
            TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase totalMemoryUsedForDownloadPerMonthModel)
        {

            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalMemoryUsedForDownloadPerMonthModel.UserId);

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>(new UserIsNotAdminException());
            }

            if (totalMemoryUsedForDownloadPerMonthModel.NumberOfMonthsToShow > 6)
            {
                return new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>(new MonthLimitException());

            }


            DateTime dt = DateTime.Now;
            List<string> previousMonths = Helpers.PreviousMonths.GetPreviousMonths(dt, totalMemoryUsedForDownloadPerMonthModel.NumberOfMonthsToShow);
            TotalMemoryUsedForDownloadPerMonthModel model = new TotalMemoryUsedForDownloadPerMonthModel();

            var returnModel = await CountTotalMemoryUsedForDownloadPerMonth(previousMonths, model);

            var responseTotalMemoryUsedForDownloadPerMonth = new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return responseTotalMemoryUsedForDownloadPerMonth;
        }

        private async Task<TotalMemoryUsedForDownloadPerMonthModel> CountTotalMemoryUsedForDownloadPerMonth(
            List<string> previousMonths, TotalMemoryUsedForDownloadPerMonthModel model)
        {
            foreach(var month in previousMonths)
            {
                if (month.Equals(Helpers.CurrentMonth.GetMonthName()))
                {
                    double totalMemoryUsedForDownloadForCurrentMonth = await CountTotalMemoryUsedForDownloadForCurrentMonth();
                    TotalMemoryUsedForDownloadPerMonthSingleModel singleModelCurrentMonth = new TotalMemoryUsedForDownloadPerMonthSingleModel(month, totalMemoryUsedForDownloadForCurrentMonth);
                    model.Content.Add(singleModelCurrentMonth);
                }
                else
                {
                    double totalMemoryUsedForDownloadInMbsForPreviousMonth = await CountTotalMemoryUsedForDownloadForPreviousMonth(month);
                    TotalMemoryUsedForDownloadPerMonthSingleModel singleModel = new TotalMemoryUsedForDownloadPerMonthSingleModel(month, totalMemoryUsedForDownloadInMbsForPreviousMonth);
                    model.Content.Add(singleModel);
                }
            }

            TotalMemoryUsedForDownloadPerMonthModel returnModel = new TotalMemoryUsedForDownloadPerMonthModel(model.Content);

            return returnModel;
        }


        private async Task<double> CountTotalMemoryUsedForDownloadForCurrentMonth()
        {
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();
            ulong countTotalMemoryUsedForDownloadPerMonthInKBs = 0;

            foreach (var user in allUsers)
            {
                countTotalMemoryUsedForDownloadPerMonthInKBs += user.FileDownloadTotal;
            }

            double countTotalMemoryUsedForDownloadPerMonthInMBs = 0;
            countTotalMemoryUsedForDownloadPerMonthInMBs = Convert.ToDouble(countTotalMemoryUsedForDownloadPerMonthInKBs / 1024.0);

            return countTotalMemoryUsedForDownloadPerMonthInMBs;
        }

        private async Task<double> CountTotalMemoryUsedForDownloadForPreviousMonth(string month)
        {
            double totalMemoryUsedForDownloadInMBs = 0;
            ulong countTotalMemoryUsedForDownloadPerMonthInKBs = 0;
            List<UserReportsData> userReportsDatas = (await _unitOfWork.UserReports.FindAllAsync(user => user.Month.Equals(month))).ToList<UserReportsData>();
            foreach (var userReport in userReportsDatas)
            {
                countTotalMemoryUsedForDownloadPerMonthInKBs += userReport.FileDownloadTotal;
            }

            totalMemoryUsedForDownloadInMBs = Convert.ToDouble(countTotalMemoryUsedForDownloadPerMonthInKBs / 1024.0);

            return totalMemoryUsedForDownloadInMBs;
        }
    }
}
