using PoroDev.Common.Models.UserReportsModels.Data;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class UserReportsRepository : GenericRepository<UserReportsData, SqlDataContext>, IUserReportsRepository
    {
        public UserReportsRepository(SqlDataContext context) : base(context)
        {
        }
    }
}
