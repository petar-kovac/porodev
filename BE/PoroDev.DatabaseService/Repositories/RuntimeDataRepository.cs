using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class RuntimeDataRepository : GenericRepository<RuntimeData, SqlDataContext>, IRuntimeDataRepository
    {
        public RuntimeDataRepository(SqlDataContext context) : base(context)
        {
        }

        public async Task<UnitOfWorkResponseModel<List<RuntimeData>>> GetRuntimeDatasByUserId(RuntimeQueryRequestGatewayToDatabase queryRequest)
        {
            var runtimeDatas = _context.RuntimeMetadata.Where(runtime => runtime.UserId.Equals(queryRequest.UserId));

            if (queryRequest.FileId.HasValue)
                runtimeDatas = runtimeDatas.Where(runtime => runtime.FileId.Equals(queryRequest.FileId.Value));

            if (queryRequest.ExecutionTime.HasValue)
                runtimeDatas = runtimeDatas.Where(runtime => runtime.ExecutionTime == queryRequest.ExecutionTime.Value);

            if (queryRequest.ExecutionStart.HasValue)
                runtimeDatas = runtimeDatas.Where(runtime => runtime.ExecutionStart.Date == queryRequest.ExecutionStart.Value.Date);

            if (queryRequest.ExecutionOutput != null)
                runtimeDatas = runtimeDatas.Where(runtime => runtime.ExecutionOutput.Equals(queryRequest.ExecutionOutput));

            if (queryRequest.ExceptionHappened.HasValue)
                runtimeDatas = runtimeDatas.Where(runtime => runtime.ExceptionHappened == queryRequest.ExceptionHappened.Value);

            if (queryRequest.Arguments != null)
                runtimeDatas = runtimeDatas.Where(runtime => runtime.Arguments.Equals(queryRequest.Arguments));

            var responseModel = new UnitOfWorkResponseModel<List<RuntimeData>>(runtimeDatas.ToList());

            return responseModel;
        }
    }
}