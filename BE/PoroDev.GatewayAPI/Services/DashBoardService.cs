using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.Constats;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    //@author Bosko Gogic
    //In this case I put upper B to make difference between service inside APIGateway and service inside PoroDev.DashboardService
    public class DashBoardService : IDashBoardService
    {
        private readonly IRequestClient<TotalNumberOfUsersRequestGatewayToService> _totalNumberOfUsersClient;
        private readonly IRequestClient<TotalNumberOfUploadedFilesRequestGatewayToService> _totalNumberOfUploadedFilesClient;
        private readonly IRequestClient<TotalNumberOfDeletedFilesRequestGatewayToService> _totalNumberOfDeletedFilesClient;
        private readonly IRequestClient<TotalRunTimeForAllUsersRequestGatewayToService> _totalNumberOfRunTimeForAllUsersClient;
        private readonly IRequestClient<TotalMemoryUsedForUploadPerMonthRequestGatewayToService> _totalMemoryUsedForUploadClient;
        private readonly IRequestClient<TotalMemoryUsedForDownloadPerMonthRequestGatewayToService> _totalMemoryUsedForDownloadClient;


        public DashBoardService(
            IRequestClient<TotalNumberOfUsersRequestGatewayToService> totalNumberOfUsersClient,
            IRequestClient<TotalNumberOfUploadedFilesRequestGatewayToService> totalNumberOfUploadedFilesClient,
            IRequestClient<TotalNumberOfDeletedFilesRequestGatewayToService> totalNumberOfDeletedFilesClient,
            IRequestClient<TotalRunTimeForAllUsersRequestGatewayToService> totalNumberOfRunTimeForAllUsersClient,
            IRequestClient<TotalMemoryUsedForUploadPerMonthRequestGatewayToService> totalMemoryUsedForUploadClient,
            IRequestClient<TotalMemoryUsedForDownloadPerMonthRequestGatewayToService> totalMemoryUsedForDownloadClient)
        {
            _totalNumberOfUsersClient = totalNumberOfUsersClient;
            _totalNumberOfUploadedFilesClient = totalNumberOfUploadedFilesClient;
            _totalNumberOfDeletedFilesClient = totalNumberOfDeletedFilesClient;
            _totalNumberOfRunTimeForAllUsersClient = totalNumberOfRunTimeForAllUsersClient;
            _totalMemoryUsedForUploadClient = totalMemoryUsedForUploadClient;
            _totalMemoryUsedForDownloadClient = totalMemoryUsedForDownloadClient; 
        }


        public async Task<TotalRunTimeForAllUsersModel> GetTotalRunTimeForAllUsers(TotalRunTimeForAllUsersRequestGatewayToService totalRunTimeForAllUsersModel)
        {
            var responseContext = await _totalNumberOfRunTimeForAllUsersClient.GetResponse<CommunicationModel<TotalRunTimeForAllUsersModel>>(totalRunTimeForAllUsersModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }

            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalNumberOfUploadedFilesModel> GetTotalNumberOfUploadedFiles(TotalNumberOfUploadedFilesRequestGatewayToService totalNumberOfUploadedFilesModel)
        {
            var responseContext = await _totalNumberOfUploadedFilesClient.GetResponse<CommunicationModel<TotalNumberOfUploadedFilesModel>>(totalNumberOfUploadedFilesModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }

            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalNumberOfDeletedFilesModel> GetTotalNumberOfDeletedFiles(TotalNumberOfDeletedFilesRequestGatewayToService totalNumberOfDeletedFilesModel)
        {
            var responseContext = await _totalNumberOfDeletedFilesClient.GetResponse<CommunicationModel<TotalNumberOfDeletedFilesModel>>(totalNumberOfDeletedFilesModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }

            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalNumberOfUsersModel> GetTotalNumberOfUsers(TotalNumberOfUsersRequestGatewayToService totalNumberOfUsersModel)
        {
            var responseContext = await _totalNumberOfUsersClient.GetResponse<CommunicationModel<TotalNumberOfUsersModel>>(totalNumberOfUsersModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }

            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalMemoryUsedForUploadPerMonthModel> GetTotalMemoryUsedForUploadPerMonth(TotalMemoryUsedForUploadPerMonthRequestGatewayToService totalMemoryUsedForUploadModel)
        {
            var responseContext = await _totalMemoryUsedForUploadClient.GetResponse<CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>>(totalNumberOfUsersModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }

            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<TotalMemoryUsedForDownloadPerMonthModel> GetTotalMemoryUsedForDownloadPerMonth(TotalMemoryUsedForDownloadPerMonthRequestGatewayToService totalMemoryUsedForDownloadModel)
        {
            var responseContext = await _totalMemoryUsedForDownloadClient.GetResponse<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>>(totalMemoryUsedForDownloadModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }

            var response = responseContext.Message.Entity;

            return response;
        }
    }
}
