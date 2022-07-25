using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;

namespace PoroDev.StorageService.Services.Contracts
{
    public interface IStorageService
    {
        Task<CommunicationModel<FileUploadResponse>> UploadFile(FileUploadRequestServiceToDatabase uploadModel);

        Task<CommunicationModel<FileDownloadMessage>> DownloadFile(FileDownloadRequestServiceToDatabase downloadModel);

        Task<CommunicationModel<FileReadModel>> ReadFiles(FileReadRequestServiceToDatabase readModel);

        Task<CommunicationModel<FileDeleteMessage>> DeleteFile(FileDeleteRequestServiceToDatabase deleteModel);

        Task<CommunicationModel<List<FileQueryModel>>> Query(FileQueryServiceToDatabase queryRequest);
    }
}