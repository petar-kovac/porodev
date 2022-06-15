﻿using AutoMapper;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using static PoroDev.Database.Constants.Constants;
using PoroDev.Common.Contracts.Update;

namespace PoroDev.Database.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
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
        }

        private bool ValidateUserDeletion(DataUserModel src)
        {
            return !string.IsNullOrEmpty(src.Id.ToString());
        }
    }
}