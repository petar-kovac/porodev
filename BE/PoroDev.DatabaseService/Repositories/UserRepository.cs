using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Models.UnitOfWorkResponse;
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

        public async Task<UnitOfWorkResponseModel<DataUserModel>> GetUserByIdWithRuntimeDatas(Guid id)
        {
            var userModel = await _context.Set<DataUserModel>().Include(x => x.runtimeDatas).FirstOrDefaultAsync(x => x.Id.Equals(id));
            var responseModel = new UnitOfWorkResponseModel<DataUserModel>()
            {
                Entity = userModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };
            return responseModel;
        }
    }
}