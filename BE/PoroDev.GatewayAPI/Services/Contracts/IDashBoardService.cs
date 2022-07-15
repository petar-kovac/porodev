using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IDashBoardService
    {
        public Task<TotalRunTimeForAllUsersModel> GetTotalRunTimeForAllUsers(TotalRunTimeForAllUsersRequestGatewayToService totalRunTimeForAllUsersModel);

        public Task<TotalNumberOfUploadedFilesModel> GetTotalNumberOfUploadedFiles(TotalNumberOfUploadedFilesRequestGatewayToService totalNumberOfUploadedFilesModel);

        public Task<TotalNumberOfDeletedFilesModel> GetTotalNumberOfDeletedFiles(TotalNumberOfDeletedFilesRequestGatewayToService totalNumberOfDeletedFilesModel);

        public Task<TotalNumberOfUsersModel> GetTotalNumberOfUsers(TotalNumberOfUsersRequestGatewayToService totalNumberOfUsersModel);


    }
}
