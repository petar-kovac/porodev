using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface INotificationDataRepository : IGenericRepository<NotificationDataModel>
    {

        Task<List<DataUserModel>> GetAllUsersToBeNotified(int day, int hour);
    }
}
