using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
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
            DataUserModel user = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);
            string currentMonth = GetMonthName();

            if (user.Role == 0)
            {
                TotalRunTimePerMonthModel returnModel = new TotalRunTimePerMonthModel();
                returnModel.TotalRunTime = await CountTotalMemoryUsedForUploadPerMonth();
                returnModel.Month = currentMonth;

                var response = new CommunicationModel<TotalRunTimePerMonthModel>()
                {
                    Entity = returnModel,
                    ExceptionName = null,
                    HumanReadableMessage = null
                };

                await context.RespondAsync<CommunicationModel<TotalRunTimePerMonthModel>>(response);
            }
            else
            {
                string exceptionType = nameof(UserIsNotAdminException);
                string humanReadableMessage = "User must be admin!";

                var resposneException = new CommunicationModel<TotalRunTimePerMonthModel>()
                {
                    Entity = null,
                    ExceptionName = exceptionType,
                    HumanReadableMessage = humanReadableMessage
                };

                await context.RespondAsync(resposneException);
            }

            throw new NotImplementedException();
        }

        public async Task<ushort> CountTotalMemoryUsedForUploadPerMonth()
        {
            List<DataUserModel> allUsers = (await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""))).ToList<DataUserModel>();
            ushort countTotalRunTimePerMonth = 0;

            foreach (var user in allUsers)
            {
                countTotalRunTimePerMonth += user.RuntimeTotal;
            }

            return countTotalRunTimePerMonth;
        }

        public string GetMonthName()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("MMMM");
        }
    }
}