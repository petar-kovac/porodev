using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class RuntimeQueryService : IRuntimeQueryService
    {
        private readonly IRequestClient<RuntimeQueryRequestGatewayToDatabase> _queryClient;

        public RuntimeQueryService(IRequestClient<RuntimeQueryRequestGatewayToDatabase> queryClient)
        {
            _queryClient = queryClient;
        }

        public async Task<List<RuntimeData>> Query(RuntimeQueryRequestGatewayToDatabase query)
        {
            var queryContext = await _queryClient.GetResponse<CommunicationModel<List<RuntimeData>>>(query);

            if (queryContext.Message.ExceptionName is not null)
                ThrowException(queryContext.Message.ExceptionName, queryContext.Message.HumanReadableMessage);

            return queryContext.Message.Entity;
        }
    }
}