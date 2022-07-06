using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;

namespace PoroDev.StorageService.Services.Contracts
{
    public interface IStorageService
    {
        // Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model);
        Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestServiceToDatabase uploadModel);

        Task<CommunicationModel<FileDownloadMessage>> DownloadFile(FileDownloadRequestServiceToDatabase downloadModel);

        Task<CommunicationModel<FileReadModel>> ReadFiles(FileReadRequestServiceToDatabase readModel);

        Task<CommunicationModel<FileDeleteMessage>> DeleteFile(FileDeleteRequestServiceToDatabase deleteModel);
    }
}