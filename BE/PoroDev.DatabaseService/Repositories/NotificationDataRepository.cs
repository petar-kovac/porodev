using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class NotificationDataRepository : GenericRepository<NotificationDataModel, SqlDataContext>, INotificationDataRepository
    {
        public NotificationDataRepository(SqlDataContext context) : base(context)
        {
        }
    }
}
