using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class UserRepository : GenericRepository<DataUserModel, SqlDataContext>, IUserRepository
    {
        public UserRepository(SqlDataContext context) : base(context)
        {
        }
    }
}