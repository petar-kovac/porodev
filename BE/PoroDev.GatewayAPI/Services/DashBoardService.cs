using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Services
{
    //@author Bosko Gogic
    //In this case I put upper B to make difference between service inside APIGateway and service inside PoroDev.DashboardService
    public class DashBoardService : IDashBoardService
    {
        private readonly IRequestClient<TotalNumberOfUsersRequestGatewayToService> _totalNumberOfUsersClient;
        private readonly IRequestClient<TotalNumberOfUploadedFilesRequestGatewayToService> _totalNumberOfUploadedFilesClient;
        private readonly IRequestClient<TotalNumberOfDeletedFilesRequestGatewayToService> _totalNumberOfDeletedFilesClient;

        public DashBoardService(
            IRequestClient<TotalNumberOfUsersRequestGatewayToService> totalNumberOfUsersClient,
            IRequestClient<TotalNumberOfUploadedFilesRequestGatewayToService> totalNumberOfUploadedFilesClient,
            IRequestClient<TotalNumberOfDeletedFilesRequestGatewayToService> totalNumberOfDeletedFilesClient)
        {
            _totalNumberOfUsersClient = totalNumberOfUsersClient;
            _totalNumberOfUploadedFilesClient = totalNumberOfUploadedFilesClient;
            _totalNumberOfDeletedFilesClient = totalNumberOfDeletedFilesClient;
        }

        public async Task<TotalNumberOfUploadedFilesModel> GetTotalNumberOfUploadedFiles(TotalNumberOfUploadedFilesRequestGatewayToService totalNumberOfUploadedFilesModel)
        {
            var responseContext = await _totalNumberOfUploadedFilesClient.GetResponse<CommunicationModel<TotalNumberOfUploadedFilesModel>>(totalNumberOfUploadedFilesModel);
            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalNumberOfDeletedFilesModel> GetTotalNumberOfDeletedFiles(TotalNumberOfDeletedFilesRequestGatewayToService totalNumberOfDeletedFilesModel)
        {
            var responseContext = await _totalNumberOfDeletedFilesClient.GetResponse<CommunicationModel<TotalNumberOfDeletedFilesModel>>(totalNumberOfDeletedFilesModel);
            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalNumberOfUsersModel> GetTotalNumberOfUsers(TotalNumberOfUsersRequestGatewayToService totalNumberOfUsersModel)
        {
            var responseContext = await _totalNumberOfUsersClient.GetResponse<CommunicationModel<TotalNumberOfUsersModel>>(totalNumberOfUsersModel);
            var response = responseContext.Message.Entity;

            return response;
        }

    }
}
