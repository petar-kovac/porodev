using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class NotificationDataRepository : GenericRepository<NotificationDataModel, SqlDataContext>, INotificationDataRepository
    {
        public NotificationDataRepository(SqlDataContext context) : base(context)
        {
        }

        public async Task<List<DataUserModel>> GetAllUsersToBeNotified(int day, int hour)
        {
            List<DataUserModel> listOfUsers = new List<DataUserModel>();
            if (DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month) == DateTime.UtcNow.Day)
            {
                listOfUsers = await FindAllAsync(day, hour);
                return listOfUsers;
            }
            else
            {
                listOfUsers = await FindAllAsync(day, hour);
                return listOfUsers;
            }
        }

        private async Task<List<DataUserModel>> FindAllAsync(int day, int hour)
        {
            var listOfUsers = await _context.NotificationData
                    .Include(obj => obj.User)
                    .Where(obj => obj.Day >= day)
                    .Where(obj => obj.Hour == hour)
                    .Where(obj => obj.UserId.Equals(obj.User.Id))
                    .Select(obj => obj.User)
                    .ToListAsync();
            return listOfUsers;
        }
    }
}
