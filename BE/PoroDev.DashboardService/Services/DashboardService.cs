using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;
using PoroDev.DashboardService.Services.Contracts;

namespace PoroDev.DashboardService.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRequestClient<TotalNumberOfUsersRequestServiceToDatabase> _totalNumberOfUsersClient;
        private readonly IRequestClient<TotalNumberOfUploadedFilesRequestServiceToDatabase> _totalNumberOfUploadedFilesClient;
        private readonly IRequestClient<TotalNumberOFDeletedFilesRequestServiceToDatabase> _totalNumberOfDeletedFilesClient;
        private readonly IRequestClient<TotalRunTimeForAllUsersRequestServiceToDatabase> _totalRunTimeForAllUsersClient;
        private readonly IRequestClient<TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase> _totalMemoryUsedForDownloadPerMonthClient;
        private readonly IRequestClient<TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase> _totalMemoryUsedForUploadPerMonthClient;

        public DashboardService
            (IRequestClient<TotalNumberOfUsersRequestServiceToDatabase> totalNumberOfUsersClient, 
            IRequestClient<TotalNumberOfUploadedFilesRequestServiceToDatabase> totalNumberOfUploadedFilesClient,
            IRequestClient<TotalNumberOFDeletedFilesRequestServiceToDatabase> totalNumberOfDeletedFilesClient,
            IRequestClient<TotalRunTimeForAllUsersRequestServiceToDatabase> totalRunTimeForAllUsersClient,
            IRequestClient<TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase> totalMemoryUsedForDownloadPerMonthClient,
            IRequestClient<TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase> totalMemoryUsedForUploadPerMonthClient)
        {
            _totalNumberOfUsersClient = totalNumberOfUsersClient;
            _totalNumberOfUploadedFilesClient = totalNumberOfUploadedFilesClient;
            _totalNumberOfDeletedFilesClient = totalNumberOfDeletedFilesClient;
            _totalRunTimeForAllUsersClient = totalRunTimeForAllUsersClient;
            _totalMemoryUsedForDownloadPerMonthClient = totalMemoryUsedForDownloadPerMonthClient;
            _totalMemoryUsedForUploadPerMonthClient = totalMemoryUsedForUploadPerMonthClient;
        }


        public async Task<CommunicationModel<TotalRunTimeForAllUsersModel>> GetTotalRunTimeForAllUsers(TotalRunTimeForAllUsersRequestServiceToDatabase totalRunTimeForUsersModel)
        {
            var response = await _totalRunTimeForAllUsersClient.GetResponse<CommunicationModel<TotalRunTimeForAllUsersModel>>(totalRunTimeForUsersModel);

            return response.Message;
        }

        public async Task<CommunicationModel<TotalNumberOfUploadedFilesModel>> GetTotalNumberOfUploadedFiles(TotalNumberOfUploadedFilesRequestServiceToDatabase totalNumberOfUploadedFilesModel)
        {
            var response = await _totalNumberOfUploadedFilesClient.GetResponse<CommunicationModel<TotalNumberOfUploadedFilesModel>>(totalNumberOfUploadedFilesModel);

            return response.Message;
        }

        public async Task<CommunicationModel<TotalNumberOfDeletedFilesModel>> GetTotalNumberOfDeletedFiles(TotalNumberOFDeletedFilesRequestServiceToDatabase totalNumberOfDeletedFiles)
        {
            var response = await _totalNumberOfDeletedFilesClient.GetResponse<CommunicationModel<TotalNumberOfDeletedFilesModel>>(totalNumberOfDeletedFiles);

            return response.Message;
        }

        public async Task<CommunicationModel<TotalNumberOfUsersModel>> GetTotalNumberOfUsers(TotalNumberOfUsersRequestServiceToDatabase totalNumberOfUsersModel)
        {
            var response = await _totalNumberOfUsersClient.GetResponse<CommunicationModel<TotalNumberOfUsersModel>>(totalNumberOfUsersModel);

            return response.Message;
        }

        public async Task<CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>> GetTotalMemoryUsedForUploadPerMonth(TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase totalMemoryUsedForUploadModel)
        {
            var response = await _totalMemoryUsedForUploadPerMonthClient.GetResponse<CommunicationModel<TotalMemoryUsedForUploadPerMonthModel>>(totalMemoryUsedForUploadModel);

            return response.Message;
        }

        public async Task<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>> GetTotalMemoryUsedForDownloadPerMonth(TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase totalMemoryUsedForDownloadModel)
        {
           var response = await _totalMemoryUsedForDownloadPerMonthClient.GetResponse<CommunicationModel<TotalMemoryUsedForDownloadPerMonthModel>>(totalMemoryUsedForDownloadModel);

           return response.Message;
        }
    }
}
