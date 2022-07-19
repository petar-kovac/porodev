﻿using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Models.RuntimeModels.Data;
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

            //CreateMap<GridFSFileInfo<ObjectId>, FileQueryModel>()
            //    .ForMember(dst => dst.Id, options => options.MapFrom(src => src.Id.ToString()))
            //    .ForMember(dst => dst.Length, options => options.MapFrom(src => (ulong)src.Length))
            //    .ForMember(dst => dst.ContentType, opt => opt.MapFrom(src => GetMetadata(src, "ContentType")))
            //    .ForMember(dst => dst.UserName, opt => opt.Ignore())
            //    .ForMember(dst => dst.UserLastname, opt => opt.Ignore());

            //CreateMap<List<GridFSFileInfo<ObjectId>>, List<FileQueryModel>>();
        }

        //private string GetMetadata(GridFSFileInfo<ObjectId> doc, string metadataName)
        //{
        //    var metadataTypeReturn = doc.Metadata.GetValue(metadataName).ToString();

        //    return metadataTypeReturn;
        //}

        private bool ValidateUserDeletion(DataUserModel src)
        {
            return !string.IsNullOrEmpty(src.Id.ToString());
        }
    }
}