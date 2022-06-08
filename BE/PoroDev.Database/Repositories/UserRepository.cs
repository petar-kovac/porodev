using PoroDev.Common.Models.UserModels;
using PoroDev.Database.Data;
using PoroDev.Database.Repositories.Contracts;

namespace PoroDev.Database.Repositories
{
    public class UserRepository : GenericRepository<DataUserModel, SqlDataContext>, IUserRepository
    {
        public UserRepository(SqlDataContext context) : base(context)
        {
        }
    }
}