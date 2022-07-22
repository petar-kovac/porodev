using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IDashBoardService
    {
        public Task<TotalRunTimeForAllUsersModel> GetTotalRunTimeForAllUsers(TotalRunTimeForAllUsersRequestGatewayToService totalRunTimeForAllUsersModel);

        public Task<TotalNumberOfUploadedFilesModel> GetTotalNumberOfUploadedFiles(TotalNumberOfUploadedFilesRequestGatewayToService totalNumberOfUploadedFilesModel);

        public Task<TotalNumberOfDeletedFilesModel> GetTotalNumberOfDeletedFiles(TotalNumberOfDeletedFilesRequestGatewayToService totalNumberOfDeletedFilesModel);

        public Task<TotalNumberOfUsersModel> GetTotalNumberOfUsers(TotalNumberOfUsersRequestGatewayToService totalNumberOfUsersModel);

        public Task<TotalRunTimePerMonthModel> GetTotalRuntimePerMonth(TotalRunTimePerMonthRequestGatewayToService totalRunTimePerMonthModel);

        public Task<TotalMemoryUsedForUploadPerMonthModel> GetTotalMemoryUsedForUploadPerMonth(TotalMemoryUsedForUploadPerMonthRequestGatewayToService totalMemoryUsedForUploadModel);

        public Task<TotalMemoryUsedForDownloadPerMonthModel> GetTotalMemoryUsedForDownloadPerMonth(TotalMemoryUsedForDownloadPerMonthRequestGatewayToService totalMemoryUsedForDownloadModel);
    }
}