using AutoMapper;
using Data.Access.Layer.Models;
using PoroDev.UserManagementService.Models.UserModels;

namespace PoroDev.UserManagementService.Mapper
{
    public class MapperBALProfiles
    {
        public class UserModelProfile : Profile
        {
            public UserModelProfile()
            {
                CreateMap<UserCreateRequestModel, DataUserModel>().ReverseMap();
                CreateMap<DataUserModel, UserLoginResponseModel>();
                CreateMap<DataUserModel, UserRegisterResponseModel>();
            }
        }
    }
}