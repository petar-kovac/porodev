using AutoMapper;
using Business.Access.Layer.Models.UserModels;
using Data.Access.Layer.Models;

namespace Business.Access.Layer.Mapper
{
    public class MapperBALProfiles
    {
        public class UserModelProfile : Profile
        {
            public UserModelProfile()
            {
                CreateMap<BusinessUserModel, DataUserModel>().ReverseMap();
                CreateMap<DataUserModel, UserLoginResponseModel>();
            }
        }
    }
}