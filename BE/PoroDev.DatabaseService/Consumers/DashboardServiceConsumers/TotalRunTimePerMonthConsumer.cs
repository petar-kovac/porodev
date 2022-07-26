using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalRunTimePerMonthConsumer : IConsumer<TotalRunTimePerMonthRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalRunTimePerMonthConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalRunTimePerMonthRequestServiceToDatabase> context)
        {
            var responseModel = await TotalMemoryUsedForRunTimePerMonth(context.Message);

            await context.RespondAsync<CommunicationModel<TotalRunTimePerMonthModel>>(responseModel);
        }

        private async Task<CommunicationModel<TotalRunTimePerMonthModel>> TotalMemoryUsedForRunTimePerMonth(
           TotalRunTimePerMonthRequestServiceToDatabase totalMemoryUsedForRunTimePerMonthModel)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalMemoryUsedForRunTimePerMonthModel.UserId);

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalRunTimePerMonthModel>(new UserIsNotAdminException());
            }

            DateTime dt = DateTime.Now;
            List<string> previousMonths = Helpers.PreviousMonths.GetPreviousMonths(dt, totalMemoryUsedForRunTimePerMonthModel.NumberOfMonthsToShow);
            TotalRunTimePerMonthModel model = new TotalRunTimePerMonthModel();

            var returnModel = await CountTotalRunTimePerMonth(previousMonths, model);

            var responseTotalMemoryUsedForUploadPerMonth = new CommunicationModel<TotalRunTimePerMonthModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return responseTotalMemoryUsedForUploadPerMonth;
        }

        private async Task<TotalRunTimePerMonthModel> CountTotalRunTimePerMonth(
            List<string> previousMonths, TotalRunTimePerMonthModel model)
        {
            foreach (var month in previousMonths)
            {
                if (month.Equals(Helpers.CurrentMonth.GetMonthName()))
                {
                    int totalRunTimeForCurrentMonth = await CountTotalRunTimeForCurrentMonth();
                    TotalRunTimePerMonthSingleModel singleModelCurrentMonth = new TotalRunTimePerMonthSingleModel(month, totalRunTimeForCurrentMonth);
                    model.Content.Add(singleModelCurrentMonth);
                }
                else
                {
                    int totalRunTimeForPreviousMonth = await CountTotalRunTimeForPreviousMonth(month);
                    TotalRunTimePerMonthSingleModel singleModel = new TotalRunTimePerMonthSingleModel(month, totalRunTimeForPreviousMonth);
                    model.Content.Add(singleModel);
                }
            }

            TotalRunTimePerMonthModel returnModel = new TotalRunTimePerMonthModel(model.Content);

            return returnModel;
        }

        private async Task<int> CountTotalRunTimeForCurrentMonth()
        {
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();

            int countTotalRunTimePerMonth = 0;

            foreach (var user in allUsers)
            {
                countTotalRunTimePerMonth += user.RuntimeTotal;
            }

            return countTotalRunTimePerMonth;
        }

        private async Task<int> CountTotalRunTimeForPreviousMonth(string month)
        {
            int totalRunTimeForPreviousMonth = 0;
            List<UserReportsData> userReportsDatas = (await _unitOfWork.UserReports.FindAllAsync(user => user.Month.Equals(month))).ToList<UserReportsData>();
            foreach (var userReport in userReportsDatas)
            {
                totalRunTimeForPreviousMonth += userReport.RuntimeTotal;
            }

            return totalRunTimeForPreviousMonth;
        }
    }
}