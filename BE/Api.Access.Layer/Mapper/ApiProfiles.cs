using Api.Access.Layer.Models.UserModels;
using AutoMapper;
using Business.Access.Layer.Models.UserModels;

namespace Api.Access.Layer.Mapper
{
    public class ApiProfiles
    {
        public class UserRequestProfile : Profile
        {
            public UserRequestProfile()
            {
                CreateMap<UserRequestModel, BusinessUserModel>();
            }
        }
    }
}