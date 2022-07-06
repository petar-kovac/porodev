using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
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
            var queryResult = await _unitOfWork.RuntimeData.GetRuntimeDatasByUserId(context.Message);

            var reponseModel = _mapper.Map<CommunicationModel<List<RuntimeData>>>(queryResult);

            await context.RespondAsync(reponseModel);
        }
    }
}