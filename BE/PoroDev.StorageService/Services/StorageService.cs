using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;

using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Services
{
    public class StorageService : IStorageService
    {
        private readonly IRequestClient<FileUploadRequestServiceToDatabase> _uploadRequestClient;
        private readonly IRequestClient<FileDownloadRequestServiceToDatabase> _downloadRequestClient;
        private readonly IRequestClient<FileReadModel> _readRequestClient;
        private readonly IRequestClient<FileDeleteRequestServiceToDatabase> _deleteRequestClient;
        private readonly IRequestClient<FileQueryServiceToDatabase> _queryClient;
        private readonly IMapper _mapper;

        public StorageService
            (IRequestClient<FileUploadRequestServiceToDatabase> uploadRequestClient,
            IRequestClient<FileDownloadRequestServiceToDatabase> downloadRequestClient,
            IRequestClient<FileReadModel> readRequestClient,
            IRequestClient<FileDeleteRequestServiceToDatabase> deleteRequestClient,
            IRequestClient<FileQueryServiceToDatabase> queryClient,
            IMapper mapper)
        {
            _uploadRequestClient = uploadRequestClient;
            _downloadRequestClient = downloadRequestClient;
            _readRequestClient = readRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _queryClient = queryClient;
            _mapper = mapper;
        }

        public async Task<CommunicationModel<FileUploadResponse>> UploadFile(FileUploadRequestServiceToDatabase uploadModel)
        {
            var fileUploadResponseContext = await _uploadRequestClient.GetResponse<CommunicationModel<FileUploadModel>>(uploadModel,CancellationToken.None, RequestTimeout.After(m: 5));

            var fileUploadResponse = _mapper.Map<CommunicationModel<FileUploadResponse>>(fileUploadResponseContext.Message);

            return fileUploadResponse;
        }

        public async Task<CommunicationModel<FileDownloadMessage>> DownloadFile(FileDownloadRequestServiceToDatabase downloadModel)
        {
            var response = await _downloadRequestClient.GetResponse<CommunicationModel<FileDownloadMessage>>(downloadModel,CancellationToken.None, RequestTimeout.After(m: 5));

            return response.Message;
        }

        public async Task<CommunicationModel<FileReadModel>> ReadFiles(FileReadRequestServiceToDatabase readModel)
        {
            var response = await _readRequestClient.GetResponse<CommunicationModel<FileReadModel>>(readModel, CancellationToken.None, RequestTimeout.After(m: 5));

            return response.Message;
        }

        public async Task<CommunicationModel<FileDeleteMessage>> DeleteFile(FileDeleteRequestServiceToDatabase deleteModel)
        {
            var response = await _deleteRequestClient.GetResponse<CommunicationModel<FileDeleteMessage>>(deleteModel, CancellationToken.None, RequestTimeout.After(m: 5));

            return response.Message;
        }

        public async Task<CommunicationModel<List<FileQueryModel>>> Query(FileQueryServiceToDatabase queryRequest)
        {
            var response = await _queryClient.GetResponse<CommunicationModel<List<FileQueryModel>>>(queryRequest, CancellationToken.None, RequestTimeout.After(m: 1));

            return response.Message;
        }
    }
}