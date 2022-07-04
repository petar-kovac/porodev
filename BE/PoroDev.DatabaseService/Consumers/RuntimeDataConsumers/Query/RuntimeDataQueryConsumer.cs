using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.RuntimeDataConsumers.Query
{
    public class RuntimeDataQueryConsumer : BaseDbConsumer, IConsumer<RuntimeQueryRequestGatewayToDatabase>
    {
        public RuntimeDataQueryConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task Consume(ConsumeContext<RuntimeQueryRequestGatewayToDatabase> context)
        {
            var runtimeDatas = (await _unitOfWork.Users.GetUserByIdWithRuntimeDatas(context.Message.UserId)).Entity.runtimeDatas;

            var result = await _unitOfWork.RuntimeData.

            if (context.Message.ExecutionTime != 0)
                runtimeDatas = (runtimeDatas.Where(x => x.ExecutionTime == context.Message.ExecutionTime)).ToList();

            if (!context.Message.FileId.Equals(Guid.Empty))
                queries.Add(runtimeDatas.Where(x => x.FileId.Equals(context.Message.FileId)));

            if(!context.Message.ExecutionStart.Equals(DateTimeOffset.MinValue))
                queries.Add(runtimeDatas.Where(x => x.ExecutionStart.Date == context.Message.ExecutionStart.Date);


            var query4 = runtimeDatas.Where(x => x.ExecutionOutput.Equals(context.Message.ExecutionOutput));


            var query5 = runtimeDatas.Where(x => x.ExceptionHappened == context.Message.ExceptionHappened);

            var list = (query1.Union(query2.Union(query3.Union(query4.Union(query5))))).ToList();




        }
    }
}
