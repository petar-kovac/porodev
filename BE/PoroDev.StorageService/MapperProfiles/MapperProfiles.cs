using AutoMapper;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.UploadFile;

namespace PoroDev.StorageService.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CommunicationModel<FileUploadModel>, CommunicationModel<FileUploadResponse>>();

            CreateMap<IUploadRequest, FileUploadRequestServiceToDatabase>();

            CreateMap<FileUploadModel, FileUploadResponse>();
        }
    }
}