using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Services
{
    //In this case I put upper B to make difference between service inside APIGateway and service inside PoroDev.DashboardService
    public class DashBoardService : IDashBoardService
    {
        private readonly IRequestClient<TotalNumberOfUsersRequestGatewayToService> _totalNumberOfUsersClient;

        public DashBoardService(
            IRequestClient<TotalNumberOfUsersRequestGatewayToService> totalNumberOfUsersClient)
        {
            _totalNumberOfUsersClient = totalNumberOfUsersClient;
        }

        public async Task<TotalNumberOfUsersModel> GetTotalNumberOfUsers(TotalNumberOfUsersRequestGatewayToService totalNumberOfUsersModel)
        {

            var responseContext = await _totalNumberOfUsersClient.GetResponse<CommunicationModel<TotalNumberOfUsersModel>>(totalNumberOfUsersModel);
            var response = responseContext.Message.Entity;

            return response;
        
        }
    }
}
