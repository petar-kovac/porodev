using AutoMapper;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.DatabaseService.Models;

namespace PoroDev.DatabaseService.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<UnitOfWorkResponseModel<RuntimeData>, CommunicationModel<RuntimeData>>();

            CreateMap<UnitOfWorkResponseModel<List<RuntimeData>>, CommunicationModel<List<RuntimeData>>>();

            CreateMap<UnitOfWorkResponseModel<DataUserModel>, CommunicationModel<DataUserModel>>();

            CreateMap<UnitOfWorkResponseModel<SharedSpacesUsers>, CommunicationModel<SharedSpacesUsers>>();

            CreateMap<UserCreateRequestServiceToDatabase, DataUserModel>();
            CreateMap<UserUpdateRequestServiceToDatabase, DataUserModel>();

            CreateMap<UnitOfWorkResponseModel<DataUserModel>, CommunicationModel<DeleteUserModel>>();
            CreateMap<DataUserModel, DeleteUserModel>()
                .ForMember(destionation => destionation.Deleted, option => option.MapFrom(source => ValidateUserDeletion(source)));

            CreateMap<RegisterUserRequestServiceToDatabase, DataUserModel>();

            CreateMap<UnitOfWorkResponseModel<DataUserModel>, CommunicationModel<DataUserModel>>();

            CreateMap<UnitOfWorkResponseModel<DataUserModel>, CommunicationModel<LoginUserModel>>();

            CreateMap<DataUserModel, LoginUserModel>()
                .ForMember(destination => destination.Role, option => option.MapFrom(source => source.Role.ToString()))
                .ForMember(destination => destination.Department, option => option.MapFrom(source => source.Department.ToString()))
                .ForSourceMember(source => source.Id, option => option.DoNotValidate())
                .ForSourceMember(source => source.Password, option => option.DoNotValidate())
                .ForSourceMember(source => source.Salt, option => option.DoNotValidate())
                .ForSourceMember(source => source.DateCreated, option => option.DoNotValidate());

            CreateMap<UnitOfWorkResponseModel<FileUploadModel>, CommunicationModel<FileUploadModel>>();

            CreateMap<FileDownload, FileDownloadMessage>()
                .ForMember(destination => destination.File, options => options.Ignore());

            CreateMap<CreateSharedSpaceRequestServiceToDatabase, SharedSpace>();
            CreateMap<UnitOfWorkResponseModel<SharedSpace>, CommunicationModel<SharedSpace>>();

            CreateMap<AddFileToSharedSpaceServiceToDatabase, SharedSpacesFiles>();

            CreateMap<UnitOfWorkResponseModel<SharedSpacesFiles>, CommunicationModel<SharedSpacesFiles>>();
        }

        private bool ValidateUserDeletion(DataUserModel src)
        {
            return !string.IsNullOrEmpty(src.Id.ToString());
        }
    }
}