using Data.Access.Layer.Data;
using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Contracts;

namespace Data.Access.Layer.Repositories
{
    public class UserRepository : GenericRepository<DataUserModel, SqlDataContext>, IUserRepository
    {
        public UserRepository(SqlDataContext context) : base(context)
        {
        }
    }
}