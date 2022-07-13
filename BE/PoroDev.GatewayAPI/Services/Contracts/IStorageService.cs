using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.GatewayAPI.Models.StorageService;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IStorageService
    {
        Task<FileUploadResponse> UploadFile(FileUploadRequestGatewayToService uploadModel);

        Task<FileDownloadResponse> DownloadFile(FileDownloadRequestGatewayToService downloadModel);

        Task<FileReadModel> ReadFiles(FileReadRequestGatewayToService readModel);

        Task<FileDeleteMessage> DeleteFile(FileDeleteRequestGatewayToService deleteModel);
    }
}