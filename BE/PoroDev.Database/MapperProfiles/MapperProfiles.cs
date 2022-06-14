﻿using AutoMapper;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;

namespace PoroDev.Database.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<UnitOfWorkResponseModel<DataUserModel>, CommunicationModel<DataUserModel>>();
            CreateMap<UserCreateRequestServiceToDatabase, DataUserModel>();
            CreateMap<UnitOfWorkResponseModel<DataUserModel>, CommunicationModel<DeleteUserModel>>();
            CreateMap<DataUserModel, DeleteUserModel>()
                .ForMember(destionation => destionation.Deleted, option => option.MapFrom(source => ValidateUserDeletion(source)));
        }

        private bool ValidateUserDeletion(DataUserModel src)
        {
            return !string.IsNullOrEmpty(src.Id.ToString());
        }
    }
}