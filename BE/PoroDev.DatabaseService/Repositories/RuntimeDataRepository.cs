using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class RuntimeDataRepository : GenericRepository<RuntimeData, SqlDataContext>, IRuntimeDataRepository
    {
        public RuntimeDataRepository(SqlDataContext context) : base(context)
        {
        }
    }
}
