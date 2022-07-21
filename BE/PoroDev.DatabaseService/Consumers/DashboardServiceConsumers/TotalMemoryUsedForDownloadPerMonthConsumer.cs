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

        private async Task<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>> TotalMemoryUsedForDownloadPerMonth
            (TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase totalMemoryUsedForDownloadPerMonthModel)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalMemoryUsedForDownloadPerMonthModel.UserId);

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>(new UserIsNotAdminException());
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

        private async Task<TotalMemoryUsedForDownloadPerMonthModel> CountTotalMemoryUsedForDownloadPerMonth(List<string> previousMonths, TotalMemoryUsedForDownloadPerMonthModel model)
        {
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();

            foreach(var month in previousMonths)
            {
                double totalMemoryUsedForDownloadInMBs = 0;
                ulong countTotalMemoryUsedForDownloadPerMonthInKBs = 0;


                List<UserReportsData> userReportsDatas = (await _unitOfWork.UserReports.FindAllAsync(user => user.Month.Equals(month))).ToList<UserReportsData>();
                foreach(var userReport in userReportsDatas)
                {
                    countTotalMemoryUsedForDownloadPerMonthInKBs += userReport.FileDownloadTotal;
                }

                totalMemoryUsedForDownloadInMBs = Convert.ToDouble(countTotalMemoryUsedForDownloadPerMonthInKBs / 1024.0);
                TotalMemoryUsedForDownloadPerMonthSingleModel singleModel = new TotalMemoryUsedForDownloadPerMonthSingleModel(month, totalMemoryUsedForDownloadInMBs);
                model.Content.Add(singleModel);
            }

            TotalMemoryUsedForDownloadPerMonthModel returnModel = new TotalMemoryUsedForDownloadPerMonthModel(model.Content);

            return returnModel;
        }
    }
}
