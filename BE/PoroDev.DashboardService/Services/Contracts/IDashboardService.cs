using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;

namespace PoroDev.DashboardService.Services.Contracts
{
    public interface IDashboardService
    {
        public Task<CommunicationModel<TotalRunTimeForAllUsersModel>> GetTotalRunTimeForAllUsers(TotalRunTimeForAllUsersRequestServiceToDatabase totalRunTimeForUsersModel);

        public Task<CommunicationModel<TotalNumberOfUploadedFilesModel>> GetTotalNumberOfUploadedFiles(TotalNumberOfUploadedFilesRequestServiceToDatabase totalNumberOfUploadedFilesModel);

        public Task<CommunicationModel<TotalNumberOfDeletedFilesModel>> GetTotalNumberOfDeletedFiles(TotalNumberOFDeletedFilesRequestServiceToDatabase totalNumberOfDeletedFiles);

        public Task<CommunicationModel<TotalNumberOfUsersModel>> GetTotalNumberOfUsers(TotalNumberOfUsersRequestServiceToDatabase totalNumberOfUsersModel);

        public Task<TotalMemoryUsedForUploadPerMonthModel> GetTotalMemoryUsedForUploadPerMonth(TotalMemoryUsedForUploadPerMonthRequestServiceToDatabase totalMemoryUsedForUploadModel);

        public Task<TotalMemoryUsedForDownloadPerMonthModel> GetTotalMemoryUsedForDownloadPerMonth(TotalMemoryUsedForDownloadPerMonthRequestServiceToDatabase totalMemoryUsedForDownloadModel);

    }
}
