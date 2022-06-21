using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.RuntimeDataConsumers
{
    public class RuntimeDataCreateConsumer : BaseDbConsumer ,IConsumer<CommunicationModel<RuntimeData>>
    {
        public RuntimeDataCreateConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public Task Consume(ConsumeContext<CommunicationModel<RuntimeData>> context)
        {
            throw new NotImplementedException();
        }
    }
}
