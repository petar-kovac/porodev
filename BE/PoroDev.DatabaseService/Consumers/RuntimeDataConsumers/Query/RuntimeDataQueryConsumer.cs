using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.RunTime.Query;
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
        }
    }
}
