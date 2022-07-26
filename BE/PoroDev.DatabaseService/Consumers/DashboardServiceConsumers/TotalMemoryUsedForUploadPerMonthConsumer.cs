using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;
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
            var responseModel = await TotalMemoryUsedForUploadPerMonth(context.Message);

            await context.RespondAsync<CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>>(responseModel);
        }


        private async Task<CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>> TotalMemoryUsedForUploadPerMonth(
            TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase totalMemoryUsedForUploadPerMonthModel)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalMemoryUsedForUploadPerMonthModel.UserId);

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>(new UserIsNotAdminException());
            }

            DateTime dt = DateTime.Now;
            List<string> previousMonths = Helpers.PreviousMonths.GetPreviousMonths(dt, totalMemoryUsedForUploadPerMonthModel.NumberOfMonthsToShow);
            TotalMemoryUsedForUploadPerMonthModel model = new TotalMemoryUsedForUploadPerMonthModel();

            var returnModel = await CountTotalMemoryUsedForUploadPerMonth(previousMonths, model);

            var responseTotalMemoryUsedForUploadPerMonth = new CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return responseTotalMemoryUsedForUploadPerMonth;
        }
        private async Task<TotalMemoryUsedForUploadPerMonthModel> CountTotalMemoryUsedForUploadPerMonth(
            List<string> previousMonths, TotalMemoryUsedForUploadPerMonthModel model)
        {
            foreach (var month in previousMonths)
            {
                if (month.Equals(Helpers.CurrentMonth.GetMonthName()))
                {
                    double totalMemoryUsedForUploadInMbsForCurrentMonth = await CountTotalMemoryUsedForUploadForCurrentMonth();
                    TotalMemoryUsedForUploadPerMonthSingleModel singleModelCurrentMonth = new TotalMemoryUsedForUploadPerMonthSingleModel(month, totalMemoryUsedForUploadInMbsForCurrentMonth);
                    model.Content.Add(singleModelCurrentMonth);
                }
                else
                {
                    double totalMemoryUsedForUploadInMbsForPreviousMonth = await CountTotalMemoryUsedForUploadForPreviousMonth(month);
                    TotalMemoryUsedForUploadPerMonthSingleModel singleModel = new TotalMemoryUsedForUploadPerMonthSingleModel(month, totalMemoryUsedForUploadInMbsForPreviousMonth);
                    model.Content.Add(singleModel);
                }
            }

            TotalMemoryUsedForUploadPerMonthModel returnModel = new TotalMemoryUsedForUploadPerMonthModel(model.Content);

            return returnModel;
        }

        private async Task<double> CountTotalMemoryUsedForUploadForCurrentMonth()
        {
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();

            ulong countTotalMemoryUsedForUploadPerMonthInKBs = 0;

            foreach (var user in allUsers)
            {
                countTotalMemoryUsedForUploadPerMonthInKBs += user.FileUploadTotal;
            }

            double countTotalMemoryUsedForDownloadPerMonthInMBs = 0;
            countTotalMemoryUsedForDownloadPerMonthInMBs = Convert.ToDouble(countTotalMemoryUsedForUploadPerMonthInKBs / 1024.0);

            return countTotalMemoryUsedForDownloadPerMonthInMBs;
        }

        private async Task<double> CountTotalMemoryUsedForUploadForPreviousMonth(string month)
        {
            double totalMemoryUsedForUploadInMBs = 0;
            ulong countTotalMemoryUsedForUploadPerMonthInKBs = 0;
            List<UserReportsData> userReportsDatas = (await _unitOfWork.UserReports.FindAllAsync(user => user.Month.Equals(month))).ToList<UserReportsData>();

            foreach (var userReport in userReportsDatas)
            {
                countTotalMemoryUsedForUploadPerMonthInKBs += userReport.FileUploadTotal;
            }

            totalMemoryUsedForUploadInMBs = Convert.ToDouble(countTotalMemoryUsedForUploadPerMonthInKBs / 1024.0);

            return totalMemoryUsedForUploadInMBs;
        }

    }
}